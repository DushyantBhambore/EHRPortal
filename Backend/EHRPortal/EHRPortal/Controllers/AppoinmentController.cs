using App.Core.Apps.Appoinment.PatientAppoinemnt.Command;
using App.Core.Apps.Appoinment.PatientAppoinemnt.Query;
using App.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHRPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppoinmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppoinmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("PatientAppoinment")]
        public async Task<IActionResult> PatientAppoinment(PatientAppoinmentDto patientAppoinmentDto)
        {
            var result = await _mediator.Send(new
                CreateAppoinmentPatientCommand { patientAppoinmentDto = patientAppoinmentDto });
            return Ok(result);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAllProviderBySpecialisation(int id)
        {
            var result = await _mediator.Send(new GetAllProviderBySpecialisationIdQuery
            { id = id });
            return Ok(result);
        }

        [HttpGet("GetAllProvider")]
        public async Task<IActionResult> GetAllProvider()
        {
            var result = await _mediator.Send(new GetAllProviderQuery());
            return Ok(result);
        }

<<<<<<< HEAD
=======
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetPatientAppoinment(int id)
        {

            var result = await _mediator.Send(new GetPatientAppoinmentByidQueryList
            { id = id });
            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByIdAppoinment(int id)
        {
            var result = await _mediator.Send(new GetByIdPatientAppoinemntQuery
            { id = id });
            return Ok(result);
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetFees(int id)
        {
            var result = await _mediator.Send(new GetPaymentBYProviderIdQuery
            { id = id });
            return Ok(result);
        }

>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
    }
}
