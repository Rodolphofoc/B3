using Applications.Finance.Commands;
using Applications.Finance.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/B3/TaskManager")]

    public class TaskManagerController : Controller
    {
        private readonly IMediator _mediator;

        public TaskManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddTaskManagerCommand request)
        {
            return Ok(await _mediator.Send(request));
        }


        [HttpGet("id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {

            var request = new GetTaskManagerByIdQuery();
            request.SetId(id);
            return Ok(await _mediator.Send(request));
        }


        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetTaskManagerQuery();

                return Ok(await _mediator.Send(query));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [HttpPut("id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] UpdateTaskManagerCommand request, [FromQuery] Guid integrationId)
        {
            request.SetId(integrationId);
            return Ok(await _mediator.Send(request));
        }

    }
}
