using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StageAPI.Application.UnitOfWorks;

namespace StageAPI.Application.Features.Commands.Activity.CreateActivity
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommandRequest, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateActivityCommandHandler> _logger;

        public CreateActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateActivityCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the creation of a new activity based on the provided request
        /// </summary>
        /// <param name="request">The command request containing data for the new activity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of type Unit indicating success or failure</returns>
        public async Task<Result<Unit>> Handle(CreateActivityCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the request contains valid data
                if (request.CreateActivity == null)
                {
                    _logger.LogError("Activity does not contain valid data");
                    return Result<Unit>.Failure("Invalid request data");
                }
                // Map the request to an Activity entity using AutoMapper
                var activity = _mapper.Map<Domain.Entities.Activity>(request.CreateActivity);
                // Add the new activity to the write repository asynchronously
                await _unitOfWork.ActivityWriteRepository.AddAsync(activity);
                // Save changes to the database and check if the operation was successful
                var result = await _unitOfWork.SaveAsync() > 0;
                // If the save operation fails, return a failure result with an error message
                if (!result)
                {
                    _logger.LogError($"Failed to create activity with {request.CreateActivity.Title}");
                    return Result<Unit>.Failure("Failed to create activity");
                }
                // Log a success message 
                _logger.LogInformation($"Activity with Id {activity.Id} successfully created");
                // Return a success result if the operation is successful
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                // Log an error in case of an exception during activity creation
                _logger.LogError(ex, $"An error occured during activity creating. Activity name: {request.CreateActivity.Title}");
                // Return a failure result with an error message
                return Result<Unit>.Failure($"An error occured: {ex.Message}");
            }
        }
    }
}