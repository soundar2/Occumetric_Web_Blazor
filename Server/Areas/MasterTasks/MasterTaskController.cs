using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using Occumetric.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.MasterTasks
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/masterTasks")]
    [AllowAnonymous]
    public class MasterTaskController : ApiController
    {
        private readonly IMasterTaskService _masterTaskService;

        public MasterTaskController(IMasterTaskService masterTaskService)
        {
            _masterTaskService = masterTaskService;
        }

        [HttpGet("industry/{IndustryId:int}")]
        public async Task<IActionResult> GetMasterTasksForIndustry([FromRoute] int IndustryId)
        {
            try
            {
                var result = await Task.Run(() => _masterTaskService.Index(IndustryId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("industry/{IndustryId:int}/category/{CategoryId:int}")]
        public async Task<IActionResult> GetMasterTasksForCategory([FromRoute] int IndustryId, int CategoryId)
        {
            try
            {
                var result = await Task.Run(() => _masterTaskService.Index(IndustryId, CategoryId));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("viewGet/{Id:int}")]
        public async Task<ActionResult<MasterTaskViewModel>> Get([FromRoute] int Id)
        {
            try
            {
                var result = await Task.Run(() => _masterTaskService.ViewGet(Id));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("updateGet/{Id:int}")]
        public async Task<ActionResult<UpdateMasterTaskDto>> GetForUpdate([FromRoute] int Id)
        {
            try
            {
                var result = await Task.Run(() => _masterTaskService.UpdateGet(Id));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateMasterTaskDto dto)
        {
            await Task.Run(() =>
            {
                _masterTaskService.Create(dto);
            });
            return Ok(new StringResult
            {
                Result = "Created"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMasterTaskDto dto)
        {
            await Task.Run(() => _masterTaskService.Update(dto));
            return Ok();
        }
    } // end class
}
