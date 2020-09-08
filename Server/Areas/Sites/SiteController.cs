using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Sites
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/sites")]
    [AllowAnonymous]
    [ApiController]
    public class SiteController : ApiController
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        [HttpGet("tenant/{tenantId:int}")]
        public async Task<ActionResult<SiteViewModel>> Index([FromRoute] int tenantId)
        {
            try
            {
                var result = await Task.Run(() => _siteService.Index(tenantId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("viewGet/{SiteId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<SiteViewModel>> ViewGet([FromRoute] int SiteId)
        {
            var result = await Task.Run(() => _siteService.ViewGet(SiteId));
            return Ok(result);
        }

        [HttpGet("updateGet/{SiteId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<UpdateSiteDto>> UpdateGet([FromRoute] int SiteId)
        {
            var result = await Task.Run(() => _siteService.UpdateGet(SiteId));
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateSiteDto dto)
        {
            await Task.Run(() =>
            {
                int result = _siteService.Create(dto);
            });
            return Ok(new StringResult
            {
                Result = "Created"
            });
        }
    } // end class
}
