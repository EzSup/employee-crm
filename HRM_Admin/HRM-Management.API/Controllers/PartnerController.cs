using HRM_Management.Core.DTOs.PartnerDtos;
using HRM_Management.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Management.API.Controllers
{
    [Route("api/partners")]
    [Authorize]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly ILogger<PartnerController> _logger;
        private readonly IPartnerService _partnerService;

        public PartnerController(ILogger<PartnerController> logger, IPartnerService partnerService)
        {
            _partnerService = partnerService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PartnerCreateRequest request)
        {
            return Ok(await _partnerService.CreateAsync(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _partnerService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PartnerUpdateRequest request)
        {
            await _partnerService.UpdateAsync(request);
            return Ok();
        }
    }
}
