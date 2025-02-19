using HRM_Management.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Management.API.Controllers
{
    [Route("api/children")]
    [ApiController]
    [Authorize]
    public class ChildController : ControllerBase
    {
        private readonly IChildService _childService;
        private readonly ILogger<ChildController> _logger;

        public ChildController(ILogger<ChildController> logger, IChildService childService)
        {
            _childService = childService;
            _logger = logger;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _childService.DeleteAsync(id);
            return Ok();
        }
    }
}
