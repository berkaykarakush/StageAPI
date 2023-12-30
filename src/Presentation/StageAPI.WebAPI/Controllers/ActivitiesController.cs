using MediatR;
using Microsoft.AspNetCore.Mvc;
using StageAPI.Application.DTOs;
using StageAPI.Application.DTOs.Activity;
using StageAPI.Application.Features.Commands.Activity.CreateActivity;
using StageAPI.Application.Features.Commands.Activity.DeleteActivity;
using StageAPI.Application.Features.Commands.Activity.UpdateActivity;
using StageAPI.Application.Features.Queries.Activity.GetAllActivity;
using StageAPI.Application.Features.Queries.Activity.GetByIdActivity;
using StageAPI.Application.RequestParameters;

namespace StageAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : BaseApiController
    {
        public ActivitiesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingParams pagingParams)
        {
            return HandlePagedResult(await _mediator.Send(new GetAllActivityQueryRequest { Params = pagingParams }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult(await _mediator.Send(new GetByIdActivityQueryRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActivityDTO createActivityDTO)
        {
            return HandleResult(await _mediator.Send(new CreateActivityCommandRequest { CreateActivity = createActivityDTO }));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateActivityDTO updateActivityDTO)
        {
            return HandleResult(await _mediator.Send(new UpdateActivityCommandRequest { UpdateActivityDTO = updateActivityDTO }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await _mediator.Send(new DeleteActivityCommandRequest { Id = id }));
        }
    }
}