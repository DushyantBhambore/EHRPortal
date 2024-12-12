using App.Core.Apps.State.Command;
using App.Core.Apps.State.Query;
using App.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHRPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddState")]
        public async Task<IActionResult> AddCountry([FromBody] StateDto stateDto)
        {
            var result = await _mediator.Send(new AddStateCommand { StateDto = stateDto });
            return Ok(result);
        }
        [HttpGet("GetAllState")]
        public async Task<IActionResult> GetAllState()
        {
            var result = await _mediator.Send(new GetAllStateQuery());
            return Ok(result);
        }
        [HttpGet("GetStateById")]
        public async Task<IActionResult> GetStateById([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetStateByIdQuery { id = id });
            return Ok(result);
        }
    }
}
