using MediatR;
using StageAPI.Application.DTOs.Activity;

namespace StageAPI.Application.Features.Queries.Activity.GetByIdActivity
{
    public class GetByIdActivityQueryRequest : IRequest<Result<ActivityDTO>>
    {
        public Guid Id { get; set; }
    }
}