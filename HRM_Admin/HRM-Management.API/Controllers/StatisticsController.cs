using AutoMapper;
using HRM_Management.Core.DTOs.EmployeeDtos;
using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using HRM_Management.Dal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Management.API.Controllers
{
    [Route("api/statistics")]
    [Authorize]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public StatisticsController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet("totalEmployeesCount")]
        public async Task<IActionResult> GetTotalCount()
        {
            return Ok(await _employeesService.GetTotalCountAsync());
        }
    }
}
