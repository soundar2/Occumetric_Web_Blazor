using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Tenants
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/tenants")]
    [AllowAnonymous]
    public class TenantController : ApiController
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet("industry/{IndustryId}")]
        public async Task<IActionResult> Index([FromRoute] int IndustryId)
        {
            try
            {
                var result = await Task.Run(() => _tenantService.Index(IndustryId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] int Id)
        {
            try
            {
                var result = await Task.Run(() => _tenantService.Get(Id));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<StringResult>> Create([FromBody] CreateTenantDto dto)
        {
            var createdId = await Task.Run(() =>
            {
                return _tenantService.Create(dto);
            });
            return Ok(new StringResult
            {
                Result = createdId.ToString()
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTenantDto dto)
        {
            await Task.Run(() => _tenantService.Update(dto));
            return Ok();
        }
    } // end class
}
