using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace StageAPI.Infrastructure.Middlewares
{
    /// <summary>
    /// Middleware to control access based on IP address whitelist
    /// </summary>
    public class IPControlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IPControlMiddleware> _logger;
        public IPControlMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<IPControlMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Invoked for each request to check and control access based on IP address whitelist
        /// </summary>
        /// <param name="context">The HTTP context for the request</param>
        /// <returns>A Task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown if the remote IP address is null</exception>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Get the remote IP address from the connection or throw an exception if null
                IPAddress remoteIp = context.Connection.RemoteIpAddress ?? throw new ArgumentNullException(nameof(remoteIp));
                // Check if the remote IP address is null and return a Bad Request response if so
                if (remoteIp == null)
                {
                    _logger.LogWarning("Invalid remote IP address");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync("Bad Request");
                    return;
                }
                // Get the whitelist of IP addresses from the configuration
                var ips = _configuration.GetSection("WhiteList").AsEnumerable()
                    .Where(ip => !string.IsNullOrEmpty(ip.Value))
                    .Select(ip => ip.Value)
                    .ToList();
                // Check if the remote IP is in the whitelist and return a Forbidden response if not
                if (!ips.Where(ip => IPAddress.Parse(ip).Equals(remoteIp)).Any())
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    await context.Response.WriteAsync("This IP is not authorized to access");
                    return;
                }
                // Log a successful request with the allowed IP address
                _logger.LogInformation($"Request from {remoteIp} processed successfully. Allowed IP: {remoteIp}");
                // Continue to the next middleware in the pipeline
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                // Log an error if an exception occurs and return an Internal Server Error response
                _logger.LogError(ex, "IPControlMiddleware failed");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Internal Server Error");
                return;
            }
        }
    }
}