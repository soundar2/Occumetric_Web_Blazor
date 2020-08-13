using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Areas.Tenants;
using Occumetric.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Industries
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/tenant")]
    public class TenantController : ApiController
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet("{industryGuid}")]
        public async Task<IActionResult> Index(int industryId)
        {
            try
            {
                var result = await Task.Run(() => _tenantService.Index(industryId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
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
        public async Task<ActionResult<StringResult>> Post(CreateTenantDto dto)
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
