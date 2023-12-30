using MediatR;
using StageAPI.Application.DTOs;

namespace StageAPI.Application.Features.Commands.Activity.CreateActivity
{
    public class CreateActivityCommandRequest : IRequest<Result<Unit>>
    {
        public CreateActivityDTO CreateActivity { get; set; }
    }
}