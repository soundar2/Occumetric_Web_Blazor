using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Jobs
{
    [Route("api/v1/jobs")]
    [AllowAnonymous]
    public class JobsController : ApiController
    {
        [AllowAnonymous]
        [HttpGet("tenant/{tenantId}")]
        public async Task<IActionResult> GetJobs([FromRoute] int tenantId)
        {
            try
            {
                var result = await Mediator.Send(new GetJobsDto
                {
                    tenantId = (int)tenantId
                });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [AllowAnonymous]
        [HttpPost()]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto dto)
        {
            try
            {
                var result = await Mediator.Send(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}