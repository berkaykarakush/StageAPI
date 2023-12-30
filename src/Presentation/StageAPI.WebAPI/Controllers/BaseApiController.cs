using MediatR;
using Microsoft.AspNetCore.Mvc;
using StageAPI.Application;
using StageAPI.WebAPI.Extensions;

namespace StageAPI.WebAPI.Controllers
{
    /// <summary>
    /// Base API controller class.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method used to handle the result of a MediatR operation.
        /// </summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="result">Result of the MediatR operation.</param>
        /// <returns>ActionResult.</returns>
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            // Return 404 Not Found if the result is null.
            if (result == null) return NotFound();
            // If the operation is successful, return 200 OK with the value, otherwise return 404 Not Found.
            if (result.IsSuccess)
                return result.Value != null ? Ok(result.Value) : NotFound();
            // If the operation is not successful, return 400 Bad Request with the error message.
            return BadRequest(result.Error);
        }
        /// <summary>
        /// Method used to handle the result of a paged MediatR operation.
        /// </summary>
        /// <typeparam name="T">Type of the paged result.</typeparam>
        /// <param name="result">Result of the paged MediatR operation.</param>
        /// <returns>ActionResult.</returns>
        protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
        {
            // Return 404 Not Found if the result is null.
            if (result == null) return NotFound();
            // If the operation is successful, check if the value is not null.
            if (result.IsSuccess)
            {
                if (result.Value != null)
                {
                    Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize, result.Value.TotalCount, result.Value.TotalPages);
                    return Ok(result.Value);
                }
                else
                {
                    // If the value is null, return 404 Not Found.
                    return NotFound();
                }
            }
            // If the operation is not successful, return 400 Bad Request with the error message.
            return BadRequest(result.Error);
        }
    }
}