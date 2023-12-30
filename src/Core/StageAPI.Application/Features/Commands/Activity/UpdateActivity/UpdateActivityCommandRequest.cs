using MediatR;
using StageAPI.Application.DTOs.Activity;

namespace StageAPI.Application.Features.Commands.Activity.UpdateActivity
{
    public class UpdateActivityCommandRequest : IRequest<Result<Unit>>
    {
        public UpdateActivityDTO UpdateActivityDTO { get; set; }
    }
}