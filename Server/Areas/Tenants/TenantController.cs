using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Tenants
{
    [Route("api/v1/tenant")]
    public class TenantController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new GetTenantsDto());
            return Ok(result);
        }

        [HttpGet("show/{guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Show([FromRoute] string guid)
        {
            var result = await Mediator.Send(new ShowTenantDto
            {
                guid = guid
            });
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateTenantDto dto)
        {
            var result = await Mediator.Send(dto);
            return Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromBody] UpdateTenantDto dto)
        {
            return Ok(await Mediator.Send(dto));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete("{guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete([FromRoute] string guid)
        {
            await Mediator.Send(new DeleteTenantDto
            {
                guid = guid
            });
            return NoContent();
        }

        //
    }
}