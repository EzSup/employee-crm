using HRM_Management.Core.DTOs.PersonDtos;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM_Management.API.Controllers
{
    [Route("api/applications")]
    [Authorize]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonsService _personsService;

        public PersonController(IPersonsService personsService, ILogger<PersonController> logger)
        {
            _personsService = personsService;
            _logger = logger;
        }

        [HttpGet("notApprovedForms")]
        public async Task<IActionResult> GetNotApprovedForms()
        {
            return Ok(await _personsService.GetAllNotApprovedFormsAsync());
        }

        [HttpGet("form/{id}")]
        public async Task<IActionResult> GetFormById(int id)
        {
            return Ok(await _personsService.GetFormByIdAsync(id));
        }

        [HttpGet("cv/{id}")]
        public async Task<IActionResult> GetCVOfForm(int id)
        {
            return Ok(await _personsService.GetDocumentAsync(id, DocumentType.CV));
        }

        [HttpGet("passportScan/{id}")]
        public async Task<IActionResult> GetPassportScanOfForm(int id)
        {
            return Ok(await _personsService.GetDocumentAsync(id, DocumentType.PassportScan));
        }

        [HttpPost("approveForm/{formId}")]
        public async Task<IActionResult> ApproveForm(int formId)
        {
            var hirerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _personsService.ApproveFormAsync(formId, hirerId);
            return Ok();
        }

        [HttpPut("form/{id}")]
        public async Task<IActionResult> UpdateForm(int id, [FromBody] FormUpdateRequest form)
        {
            
            await _personsService.UpdateFormAsync(form);
            return Ok();
        }

        [HttpDelete("reject/{formId}")]
        public async Task<IActionResult> RejectForm(int formId)
        {
            await _personsService.RejectFormAsync(formId);
            return Ok();
        }
    }
}