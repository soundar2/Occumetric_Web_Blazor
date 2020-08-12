using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Industries
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/industry")]
    public class IndustryController : ApiController
    {
        private readonly IIndustryService _industryService;

        public IndustryController(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await Task.Run(() => _industryService.GetIndustries());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<StringResult>> Post(CreateIndustryDto dto)
        {
            var result = await Task.Run(() =>
            {
                return _industryService.CreateIndustry(dto);
            });
            return Ok(new StringResult
            {
                Result = result
            });
        }
    } // end class
}
