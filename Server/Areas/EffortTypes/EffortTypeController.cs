using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Occumetric.Server.Areas.Shared;
using System;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.EffortTypes
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/effortTypes")]
    [AllowAnonymous]
    public class EffortTypeController : ApiController
    {
        private readonly IEffortTypeService _effortTypeService;

        public EffortTypeController(IEffortTypeService effortTypeService)
        {
            _effortTypeService = effortTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await Task.Run(() => _effortTypeService.GetEffortTypes());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    } // end class
}
