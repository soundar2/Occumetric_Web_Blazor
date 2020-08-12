using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Snooks
{
    [Route("api/v1/snooks")]
    [AllowAnonymous]
    public class SnooksController : ApiController
    {
        [HttpGet("percentages")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSnooksPercentages([FromBody] GetSnooksPercentagesDto dto)
        {
            try
            {
                return Ok(await Mediator.Send(dto));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}