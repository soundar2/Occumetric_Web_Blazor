using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.TaskCategories
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/taskCategories")]
    [AllowAnonymous]
    public class TaskCategoryController : ApiController
    {
        private readonly ITaskCategoryService _taskCategoryService;

        public TaskCategoryController(ITaskCategoryService taskCategoryService)
        {
            _taskCategoryService = taskCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await Task.Run(() => _taskCategoryService.Index());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{Id:int}")]
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
        public IActionResult Create([FromBody] CreateTaskCategoryDto dto)
        {
            _taskCategoryService.Create(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTaskCategoryDto dto)
        {
            await Task.Run(() => _taskCategoryService.Update(dto));
            return Ok();
        }
    } // end class
}
