using MediatR;

namespace StageAPI.Application.Features.Commands.Activity.DeleteActivity
{
    public class DeleteActivityCommandRequest : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}