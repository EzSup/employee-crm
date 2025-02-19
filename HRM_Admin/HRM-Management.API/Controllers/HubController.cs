using HRM_Management.Core.DTOs.HubDtos;
using HRM_Management.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Management.API.Controllers
{
    [Route("api/hub")]
    [Authorize]
    [ApiController]
    public class HubController : Controller
    {
        private readonly IEmployeesService _employeesService;
        private readonly IHubService _hubService;

        public HubController(IHubService hubService, IEmployeesService employeesService)
        {
            _hubService = hubService;
            _employeesService = employeesService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _hubService.GetByIdWithJoinsAsync(id));
        }

        [HttpGet("freeEmployees")]
        public async Task<IActionResult> GetFreeEmployees()
        {
            return Ok(await _employeesService.GetNotAssignedToHubEmployeesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(HubCreateRequest request)
        {
            return Ok(await _hubService.AddAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update(HubUpdateRequest request)
        {
            await _hubService.UpdateAsync(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hubService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("assignMembers")]
        public async Task<IActionResult> AssignMembers(int hubId, [FromBody] int[] employeesIds)
        {
            await _hubService.AssignMembersAsync(hubId, employeesIds);
            return Ok();
        }

        [HttpDelete("dismissMembers")]
        public async Task<IActionResult> DismissMembers(int hubId, [FromBody] int[] employeesIds)
        {
            await _hubService.DismissMembersAsync(hubId, employeesIds);
            return Ok();
        }
    }
}
