using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.JobTasks;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Jobs
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/jobs")]
    [AllowAnonymous]
    [ApiController]
    public class JobController : ApiController
    {
        private readonly IJobService _jobService;
        private readonly IJobTaskService _jobTaskService;

        public JobController(IJobService jobService,
            IJobTaskService jobTaskService)
        {
            _jobService = jobService;
            _jobTaskService = jobTaskService;
        }

        [HttpGet("tenant/{tenantId:int}")]
        public async Task<ActionResult<JobViewModel>> Index([FromRoute] int tenantId)
        {
            try
            {
                var result = await Task.Run(() => _jobService.Index(tenantId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("counts")]
        [AllowAnonymous]
        public async Task<ActionResult<TenantSummary>> GetJobCounts()
        {
            var result = await Task.Run(() => _jobService.GetJobCountsByTenant());
            return Ok(result);
        }

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

        [HttpPost("addNewTasks/{JobId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> AddNewTasks([FromRoute] int JobId, [FromBody] List<int> MasterTaskIds)
        {
            await Task.Run(() =>
            {
                bool result = _jobService.AddNewTasksToJob(JobId, MasterTaskIds);
            });
            return Ok(new StringResult
            {
                Result = "Added"
            });
        }

        [HttpGet("jobTasks/forUpdate")]
        public async Task<ActionResult<UpdateJobTaskDto>> GetJobTask([FromRoute] int TaskId)
        {
            try
            {
                var result = await Task.Run(() => _jobTaskService.ViewGet(TaskId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("jobTasks")]
        public async Task<ActionResult<JobTaskViewModel>> UpdateGetJobTask([FromRoute] int TaskId)
        {
            try
            {
                var result = await Task.Run(() => _jobTaskService.ViewGet(TaskId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("jobTasks")]
        public async Task<ActionResult<JobTaskViewModel>> ViewGetJobTask([FromRoute] int TaskId)
        {
            try
            {
                var result = await Task.Run(() => _jobTaskService.ViewGet(TaskId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("jobTasks")]
        public async Task<IActionResult> UpdateJobTask(UpdateJobTaskDto dto)
        {
            await Task.Run(() => _jobTaskService.Update(dto));
            return Ok();
        }
    } // end class
}
