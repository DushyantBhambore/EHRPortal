﻿using App.Core.Apps.Appoinment.PatientAppoinemnt.Command;
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
                CreateAppoinmentPatientCommand { patientAppoinmentDto=patientAppoinmentDto });
            return Ok(result);
        }
    }
}