using App.Core.Apps.State.Query;
using App.Core.Apps.UserType.Command;
using App.Core.Apps.UserType.Query;
using App.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHRPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddUserType")]
        public async Task<IActionResult> AddUserType([FromBody] UserTypeDto userTypeDto)
        {
            var result = await _mediator.Send(new AddUserTypeCommand { UserTypeDto = userTypeDto });
            return Ok(result);
        }
        [HttpGet("GetAllUserType")]
        public async Task<IActionResult> GetAllUserType()
        {
            var result = await _mediator.Send(new GetAllUserTypeQuery ());
            return Ok(result);
        }
    }
}
