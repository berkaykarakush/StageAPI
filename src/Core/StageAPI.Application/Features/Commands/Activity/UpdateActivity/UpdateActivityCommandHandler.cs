using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StageAPI.Application.UnitOfWorks;

namespace StageAPI.Application.Features.Commands.Activity.UpdateActivity
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommandRequest, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateActivityCommandHandler> _logger;
        public UpdateActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateActivityCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Handles the update of an existing activity based on the provided request
        /// </summary>
        /// <param name="request">The command request containing data for updating the activity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of type Unit indicating success or failure</returns>
        public async Task<Result<Unit>> Handle(UpdateActivityCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the existing activity by Id from the read repository asynchronously
                var activity = await _unitOfWork.ActivityReadRepository.GetByIdAsync(request.UpdateActivityDTO.Id);
                // If the activity is not found, return a failure result
                if (activity == null)
                {
                    _logger.LogWarning($"Activity with id {request.UpdateActivityDTO.Id} not found for updating");
                    return Result<Unit>.Failure($"Activity with Id {request.UpdateActivityDTO.Id} not found");
                }
                // Map the data from UpdateActivityDTO to the existing activity using AutoMapper
                _mapper.Map(request.UpdateActivityDTO, activity);
                // Save changes to the database and check if the operation was successful
                var result = await _unitOfWork.SaveAsync() > 0;
                // If the save operation fails, return a failure result with an error message
                if (!result)
                {
                    _logger.LogError($"Failed to update activity with Id {request.UpdateActivityDTO.Id}");
                    return Result<Unit>.Failure("Failed to update activity");
                }
                // Log a success message
                _logger.LogInformation($"Activity with Id {request.UpdateActivityDTO.Id} successfully updated");
                // Return a success result if the operation is successful
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                // Log an error in case of an exception during activity updating
                _logger.LogError(ex, $"An error occured during activity updating. Activity Id: {request.UpdateActivityDTO.Id}");
                // Return a failure result with an error message
                return Result<Unit>.Failure($"An error occured: {ex.Message}");
            }
        }
    }
}