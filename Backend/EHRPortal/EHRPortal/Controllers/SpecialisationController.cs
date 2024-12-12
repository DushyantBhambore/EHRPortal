using App.Core.Apps.Specialisation.Command;
using App.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHRPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialisationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SpecialisationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddSpecialisation")]
        public async Task<IActionResult> AddSpecialisation(SpecialisationDto specialisationDto)
        {
            var result = await _mediator.Send(new AddSpecialisationCommand { SpecialisationDto = specialisationDto });
            return Ok(result);
        }
    }
}
