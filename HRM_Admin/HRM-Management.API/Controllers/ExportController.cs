using HRM_Management.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Management.API.Controllers
{
    [ApiController]
    [Route("export")]
    [Authorize]
    public class ExportController : ControllerBase
    {
        private readonly IExcelService _excelService;

        public ExportController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpPost("toExcel")]
        public async Task<IActionResult> ExportExcel([FromBody] string query)
        {
            var file = await _excelService.ExportFromQueryAsync(query);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Export-{DateTime.UtcNow.ToString()}.xlsx");
        }
    }
}
