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


        [HttpGet("{integrationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromRoute] Guid integrationId)
        {

            var request = new GetTaskManagerByIdQuery();
            request.SetId(integrationId);
            return Ok(await _mediator.Send(request));
        }


        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetTaskManagerQuery();
            return Ok(await _mediator.Send(query));
        }


        [HttpPut("{integrationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put([FromBody] UpdateTaskManagerCommand request, [FromRoute] Guid integrationId)
        {
            request.SetId(integrationId);
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{integrationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] Guid integrationId)
        {
            var request = new DeleteTaskManagerCommand();
            request.SetId(integrationId);
            return Ok(await _mediator.Send(request));
        }
    }
}
