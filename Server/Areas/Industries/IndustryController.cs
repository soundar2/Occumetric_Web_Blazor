using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Industries
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/industry")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await Task.Run(() => _industryService.Get(id));
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
            var id = await Task.Run(() =>
            {
                return _industryService.Create(dto);
            });
            return Ok(new StringResult
            {
                Result = id.ToString()
            }); ;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateIndustryDto dto)
        {
            await Task.Run(() => _industryService.Update(dto));
            return Ok();
        }
    } // end class
}
