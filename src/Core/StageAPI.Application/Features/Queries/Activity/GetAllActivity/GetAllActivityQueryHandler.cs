using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StageAPI.Application.DTOs.Activity;
using StageAPI.Application.UnitOfWorks;

namespace StageAPI.Application.Features.Queries.Activity.GetAllActivity
{
    public class GetAllActivityQueryHandler : IRequestHandler<GetAllActivityQueryRequest, Result<PagedList<ActivityDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllActivityQueryHandler> _logger;
        public GetAllActivityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllActivityQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Handles the query to retrieve all activities based on the provided request
        /// </summary>
        /// <param name="request">The query request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result containing a paged list of ActivityDTOs</returns>
        public async Task<Result<PagedList<ActivityDTO>>> Handle(GetAllActivityQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve all activities from the read repository
                var query = _unitOfWork.ActivityReadRepository.GetAll(false);
                // If the query is null, log an error and return a failure result
                if (query == null)
                {
                    _logger.LogError($"Failed to retrieve activities");
                    return Result<PagedList<ActivityDTO>>.Failure($"Failed to retrieve activities");
                }
                // Map the activities to ActivityDTO using AutoMapper
                var mappedQuery = query.Select(activity => _mapper.Map<ActivityDTO>(activity));
                // Log a success message
                _logger.LogInformation($"Retrieved {mappedQuery.Count()} activities");
                // Return a success result with a paged list of ActivityDTOs
                return Result<PagedList<ActivityDTO>>.Success(await PagedList<ActivityDTO>.CreateAsync(mappedQuery, request.Params.pageNumber, request.Params.pageSize));
            }
            catch (Exception ex)
            {
                // Log an error in case of an exception during activity retrieval.
                _logger.LogError(ex, $"Error occured while retrieving activities");
                // Return a failure result with an error message
                return Result<PagedList<ActivityDTO>>.Failure($"An error occured: {ex.Message}");
            }
        }
    }
}