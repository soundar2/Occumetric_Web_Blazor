using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;

namespace Occumetric.Server.Areas.Helpers
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/helpers")]
    [ApiController]
    [AllowAnonymous]
    public class HelperController : ApiController
    {
        private readonly IHelperService _helperService;

        public HelperController(IHelperService helperService)
        {
            _helperService = helperService;
        }

        [HttpGet("states")]
        [AllowAnonymous]
        public IActionResult GetStates()
        {
            return Ok(_helperService.GetStates());
        }

        [HttpGet("effortTypes")]
        [AllowAnonymous]
        public IActionResult GetEffortTypes()
        {
            return Ok(_helperService.GetEffortTypes());
        }

        [HttpGet("liftDurationTypes")]
        [AllowAnonymous]
        public IActionResult GetLiftDurationTypes()
        {
            return Ok(_helperService.GetLiftDurationTypes());
        }

        [HttpGet("liftFrequencyTypes")]
        public IActionResult GetLiftFrequencyTypes()
        {
            return Ok(_helperService.GetLiftFrequencyTypes());
        }

        [HttpPost("nioshIndex")]
        public IActionResult CalculateNioshIndex(NioshCalculateDto dto)
        {
            return Ok(_helperService.GetNioshIndex(dto));
        }

        [HttpPost("snooks")]
        public IActionResult CalculateSnooks(SnooksCalculateDto dto)
        {
            return Ok(_helperService.CalculateSnooks(dto));
        }
    } // end class
}
