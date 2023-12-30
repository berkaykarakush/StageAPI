using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StageAPI.Application.DTOs.Activity;
using StageAPI.Application.UnitOfWorks;

namespace StageAPI.Application.Features.Queries.Activity.GetByIdActivity
{
    public class GetByIdActivityQueryHandler : IRequestHandler<GetByIdActivityQueryRequest, Result<ActivityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdActivityQueryHandler> _logger;
        public GetByIdActivityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetByIdActivityQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Handles the query to retrieve an activity by its Id
        /// </summary>
        /// <param name="request">The query request containing the Id of the activity to retrieve</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of type ActivityDTO indicating success or failure</returns>
        public async Task<Result<ActivityDTO>> Handle(GetByIdActivityQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the activity by Id from the read repository asynchronously
                var activity = await _unitOfWork.ActivityReadRepository.GetByIdAsync(request.Id);
                // If the activity is not found, log an error and return a failure result
                if (activity == null)
                {
                    _logger.LogError($"Activity with Id {request.Id}");
                    return Result<ActivityDTO>.Failure($"Activity with ID {request.Id} not found.");
                }
                //  Log a success message
                _logger.LogInformation($"Successfully retrieved activity with Id {request.Id}");
                // Return a success result with the mapped ActivityDTO
                return Result<ActivityDTO>.Success(_mapper.Map<ActivityDTO>(activity));
            }
            catch (Exception ex)
            {
                // Log an error in case of an exception during activity retrieval
                _logger.LogError(ex, $"Error occured while retrieving activity with Id {request.Id}");
                // Return a failure result with an error message
                return Result<ActivityDTO>.Failure($"An error occured: {ex.Message}");
            }
        }
    }
}