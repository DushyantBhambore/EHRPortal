using App.Core.Apps.User.Command;
using App.Core.Apps.User.Query;
using App.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHRPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;


        public UserController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpPost("PatientRegister")]
        public async Task<IActionResult> PatientRegister([FromForm] PatientDto patientDto)
        {
            var result = await _mediator.Send(new AddPatientCommand { patientDto = patientDto });
            return Ok(result);
        }
        [HttpPost("PractitionerRegister")]
        public async Task<IActionResult> PractitionerRegister([FromForm] PractitionerDto practitionerDto)
        {
            var result = await _mediator.Send(new AddPractitionerCommand { practitionerDto = practitionerDto });
            return Ok(result);
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            var result = await _mediator.Send(new LoginUserCommand { LoginDto = loginDto });
            return Ok(result);
        }
        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto verifyOtpDto)
        {
            var result = await _mediator.Send(new VerifyOtpCommand { VerifyOtp = verifyOtpDto });
            return Ok(result);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetByIdQuery { id = id });
            return Ok(result);
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var result = await _mediator.Send(new ForgetPasswordCommand { forgotEamilDto = forgotPasswordDto });
            return Ok(result);
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var result = await _mediator.Send(new ChangePasswordCommand { ChangePasswordDto = changePasswordDto });
            return Ok(result);
        }
        [HttpPost("UpdatedPatient")]
        public async Task<IActionResult> UpdatedPatient([FromForm] PatientDto patientDto)
        {
            var result = await _mediator.Send(new UpdatePatientCommand { patientDto = patientDto });
            return Ok(result);
        }

        [HttpPost("UpdatedPractitioner")]
        public async Task<IActionResult> UpdatedPractitioner([FromForm] PractitionerDto practitionerDto)
        {
            var result = await _mediator.Send(new UpdatePractitonerCommand { practitionerDto = practitionerDto });
            return Ok(result);
        }
    }
}
