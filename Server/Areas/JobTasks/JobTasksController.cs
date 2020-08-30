using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.JobTasks;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Jobs
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/jobTasks")]
    [AllowAnonymous]
    [ApiController]
    public class JobTaskController : ApiController
    {
        private readonly IJobService _jobService;
        private readonly IJobTaskService _jobTaskService;

        public JobTaskController(IJobService jobService,
            IJobTaskService jobTaskService)
        {
            _jobService = jobService;
            _jobTaskService = jobTaskService;
        }

        [HttpGet("viewGet/{TaskId:int}")]
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

        [HttpGet("updateGet/{TaskId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateGetJobTask([FromRoute] int TaskId)
        {
            try
            {
                var result = await Task.Run(() => _jobTaskService.UpdateGet(TaskId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateJobTask(UpdateJobTaskDto dto)
        {
            await Task.Run(() => _jobTaskService.Update(dto));
            return Ok();
        }

        [HttpGet("botReport/{JobId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> SimplifyTasksForBotReport([FromRoute] int JobId)
        {
            var result = await Task.Run(() =>
            {
                return _jobTaskService.SimplifyListsForBotReport(JobId);
            });
            return Ok(result);
        }
    } // end class
}
