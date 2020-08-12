using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.MasterTasks
{
    [Route("api/v1/master-task")]
    public class MasterTaskController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetMasterTasksDto()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] string searchFor)
        {
            try
            {
                return Ok(await Mediator.Send(new GetMasterTasksDto()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("show/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            try
            {
                var result = await Mediator.Send(new ShowMasterTaskDto
                {
                    id = (int)id
                });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpPut]
        //[AllowAnonymous]
        //public async Task<IActionResult> Update([FromBody] MasterTask masterTask)
        //{
        //    var result = await Mediator.Send(new ShowMasterTaskDto
        //    {
        //        id = id
        //    });
        //    return Ok(result);
        //}
    }
}