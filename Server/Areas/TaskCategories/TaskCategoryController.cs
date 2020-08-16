using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.TaskCategories
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/taskCategory")]
    [AllowAnonymous]
    public class TaskCategoryController : ApiController
    {
        private readonly ITaskCategoryService _taskCategoryService;

        public TaskCategoryController(ITaskCategoryService taskCategoryService)
        {
            _taskCategoryService = taskCategoryService;
        }

        [HttpGet("industry/{IndustryId}")]
        public async Task<IActionResult> Index([FromRoute] int IndustryId)
        {
            try
            {
                var result = await Task.Run(() => _taskCategoryService.Index(IndustryId));
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
                var result = await Task.Run(() => _taskCategoryService.Get(Id));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<StringResult>> Create([FromBody] CreateTaskCategoryDto dto)
        {
            var createdId = await Task.Run(() =>
            {
                return _taskCategoryService.Create(dto);
            });
            return Ok(new StringResult
            {
                Result = createdId.ToString()
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTaskCategoryDto dto)
        {
            await Task.Run(() => _taskCategoryService.Update(dto));
            return Ok();
        }
    } // end class
}
