using MediatR;
using Microsoft.Extensions.Logging;
using StageAPI.Application.Repositories.Activity;
using StageAPI.Application.UnitOfWorks;
using StageAPI.Persistence.Contexts;
using StageAPI.Persistence.Repositories.Activity;

namespace StageAPI.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StageAPIDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private ActivityReadRepository _activityReadRepository;
        private ActivityWriteRepository _activityWriteRepository;

        public UnitOfWork(StageAPIDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActivityReadRepository ActivityReadRepository => _activityReadRepository = _activityReadRepository ?? new ActivityReadRepository(_context, _logger);
        public IActivityWriteRepository ActivityWriteRepository => _activityWriteRepository = _activityWriteRepository ?? new ActivityWriteRepository(_context, _logger);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving changes to the database");
                throw new Exception("Error saving changes to the database", ex);
            }

        }
    }
}