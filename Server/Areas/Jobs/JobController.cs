using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Jobs
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/jobs")]
    [AllowAnonymous]
    public class JobController : ApiController
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet("tenant/{tenantId:int}")]
        public async Task<ActionResult<JobViewModel>> GetJobsForIndustry([FromRoute] int tenantId)
        {
            try
            {
                var result = await Task.Run(() => _jobService.GetJobs(tenantId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpGet("industry/{IndustryId:int}/category/{CategoryId:int}")]
        //public async Task<IActionResult> GetJobsForCategory([FromRoute] int IndustryId, int CategoryId)
        //{
        //    try
        //    {
        //        var result = await Task.Run(() => _jobService.GetJobs(IndustryId, CategoryId));
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        //[HttpGet("{Id:int}")]
        //public async Task<ActionResult<JobViewModel>> Get([FromRoute] int Id)
        //{
        //    try
        //    {
        //        var result = await Task.Run(() => _jobService.Get(Id));
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        //[HttpGet("forUpdate/{Id:int}")]
        //public async Task<ActionResult<UpdateJobDto>> GetForUpdate([FromRoute] int Id)
        //{
        //    try
        //    {
        //        var result = await Task.Run(() => _jobService.GetForUpdate(Id));
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateJobDto dto)
        {
            await Task.Run(() =>
            {
                int result = _jobService.Create(dto);
            });
            return Ok(new StringResult
            {
                Result = "Created"
            });
        }

        //[HttpPut]
        //public async Task<IActionResult> Update(UpdateJobDto dto)
        //{
        //    await Task.Run(() => _jobService.Update(dto));
        //    return Ok();
        //}
    } // end class
}
