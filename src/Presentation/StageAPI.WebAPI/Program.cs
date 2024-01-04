using Microsoft.EntityFrameworkCore;
using StageAPI.Persistence;
using StageAPI.Infrastructure;
using StageAPI.Persistence.Contexts;
using StageAPI.Application;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using Serilog.Core;
using Microsoft.AspNetCore.HttpLogging;
using StageAPI.Infrastructure.Middlewares;
var builder = WebApplication.CreateBuilder(args);
// Configure services
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddDbContext<StageAPIDbContext>();

// Add CORS Policy
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins()
    .AllowAnyHeader()
    .AllowAnyMethod()
));

// Add Serilog
Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(
        builder.Configuration.GetConnectionString("PostgreSQLConnection"),
        "logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter()},
            {"message_template", new MessageTemplateColumnWriter()},
            {"level", new LevelColumnWriter()},
            {"time_stamp",new TimestampColumnWriter()},
            {"exception", new ExceptionColumnWriter()},
            {"log_event",new LogEventSerializedColumnWriter()}
        })
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog(log);
});

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
// Enforce HTTPS
app.UseHttpsRedirection();
// Add authorization
app.UseAuthorization();
// Map controllers
app.MapControllers();
// Enable CORS Policy
app.UseCors();
// Add custom IP control middleware
app.UseMiddleware<IPControlMiddleware>();
// Seed data during application startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<StageAPIDbContext>();
        await context.Database.MigrateAsync();
        await Seed.SeedData(context);
    }
    catch (Exception ex)
    {
        // If an exception occurs during migration or seeding, log the error using a logger
        // Get the logger service for the Program class from the service provider
        var logger = services.GetRequiredService<ILogger<Program>>();
        // Log the exception with an error message
        logger.LogError(ex, "An error occurred during migration or seeding");
    }
}

app.Run();