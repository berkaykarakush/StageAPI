using MediatR;
using StageAPI.Application.DTOs.Activity;
using StageAPI.Application.RequestParameters;

namespace StageAPI.Application.Features.Queries.Activity.GetAllActivity
{
    public class GetAllActivityQueryRequest : IRequest<Result<PagedList<ActivityDTO>>>
    {
        public PagingParams Params { get; set; }
    }
}