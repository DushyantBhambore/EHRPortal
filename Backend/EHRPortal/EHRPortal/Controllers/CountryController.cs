using App.Core.Apps.Country.Command;
using App.Core.Apps.Country.Query;
using App.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHRPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddCountry")]
        public async Task<IActionResult> AddCountry([FromBody] CountryDto countryDto)
        {
            var result = await _mediator.Send(new AddCountryCommand { CountryDto = countryDto });
            return Ok(result);
        }
        [HttpGet("GetAllCountry")]
        public async Task<IActionResult> GetAllCountry()
        {
            var result = await _mediator.Send(new GetAllCountryQuery());
            return Ok(result);
        }
    }
}
