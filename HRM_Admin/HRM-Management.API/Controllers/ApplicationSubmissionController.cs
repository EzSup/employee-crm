using HRM_Management.Core.DTOs.PersonDtos;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Management.API.Controllers
{
    [Route("api/application")]
    [ApiController]
    public class ApplicationSubmissionController : ControllerBase
    {
        private readonly IPersonsService _personsService;

        public ApplicationSubmissionController(IPersonsService personsService)
        {
            _personsService = personsService;
        }

        [HttpGet("CheckUserSubmission/{telegramId}")]
        public async Task<IActionResult> CheckUserSubmission(long telegramId)
        {
            return await _personsService.CheckPersonSubmission(telegramId) ? Ok() : BadRequest();
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyForm([FromBody] ApplicationSubmitRequest request)
        {
            var resultId = await _personsService.RegisterApplicationAsync(request);
            return Ok(resultId);
        }

        [HttpPost("attachphoto/{id}")]
        public async Task<IActionResult> AttachPhotoToPerson(int id, IFormFile file)
        {
            await _personsService.AttachDocumentAsync(id, file, DocumentType.Photo);
            return Ok();
        }

        [HttpPost("attachcv/{id}")]
        public async Task<IActionResult> AttachCVToPerson(int id, IFormFile file)
        {
            await _personsService.AttachDocumentAsync(id, file, DocumentType.CV);
            return Ok();
        }

        [HttpPost("attachpassportscan/{id}")]
        public async Task<IActionResult> AttachPassportScanToPerson(int id, IFormFile file)
        {
            await _personsService.AttachDocumentAsync(id, file, DocumentType.PassportScan);
            return Ok();
        }
    }
}
