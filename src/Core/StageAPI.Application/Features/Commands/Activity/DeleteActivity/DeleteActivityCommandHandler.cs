using MediatR;
using Microsoft.Extensions.Logging;
using StageAPI.Application.UnitOfWorks;

namespace StageAPI.Application.Features.Commands.Activity.DeleteActivity
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommandRequest, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteActivityCommandHandler> _logger;

        public DeleteActivityCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteActivityCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Handles the request of type DeleteActivityCommandRequest
        /// </summary>
        /// <param name="request">The request itself</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns a Result of type Unit</returns>
        public async Task<Result<Unit>> Handle(DeleteActivityCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieves the activity based on the Id in the request
                var activity = await _unitOfWork.ActivityReadRepository.GetByIdAsync(request.Id);
                // If the activity is not found, return a failure result
                if (activity == null)
                {
                    _logger.LogWarning($"Activity with id {request.Id} not found for deleting");
                    return Result<Unit>.Failure($"Activity not found with Id {request.Id}");
                }
                // Performs the deletion of the activity
                _unitOfWork.ActivityWriteRepository.Remove(activity);
                // Performs the operation to save changes to the database
                var result = await _unitOfWork.SaveAsync() > 0;
                // Returns Result.Failure with an error message if the save operation fails
                if (!result)
                {
                    _logger.LogError($"Failed to delete activity with Id {request.Id}");
                    return Result<Unit>.Failure("Problem to delete the activity");
                }
                // Log a success message.
                _logger.LogInformation($"Activity with Id {request.Id} successfully deleted");
                // Returns a successful result if the operation is successful
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                // Log an error in case of an exception during activity deletion
                _logger.LogError(ex, $"An error occured during activity deletion. Activity Id: {request.Id}");
                // Return a failure result with an error message
                return Result<Unit>.Failure($"An error occured: {ex.Message}");
            }
        }
    }
}