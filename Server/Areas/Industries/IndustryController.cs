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
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await Task.Run(() => _industryService.Index());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(string guid)
        {
            try
            {
                var result = await Task.Run(() => _industryService.Get(guid));
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
                return _industryService.Create(dto);
            });
            return Ok(new StringResult
            {
                Result = result
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateIndustryDto dto)
        {
            await Task.Run(() => _industryService.Update(dto));
            return Ok();
        }
    } // end class
}
