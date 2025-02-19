using HRM_Management.Core.DTOs.Enums;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Management.API.Controllers
{
    [ApiController]
    [Route("subscription")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("getOfPerson/{id}")]
        public async Task<IActionResult> GetNotifications(int id)
        {
            return Ok(await _subscriptionService.GetNotificationsOfPersonAsync(id));
        }

        [HttpGet("getAvailable/{personId}")]
        public async Task<IActionResult> GetAvailableNotifications(int personId)
        {
            return Ok(await _subscriptionService.GetAvailableToSubscribeAsync(personId));
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(NotificationJob job, int personId, string cronExpression, NotificationType notificationType)
        {
            return Ok(
                await _subscriptionService.SubscribeAsync(job, personId, cronExpression, notificationType, null));
        }

        [HttpDelete]
        public async Task<IActionResult> Unsubscribe(NotificationJob job, int personId)
        {
            await _subscriptionService.UnsubscribeAsync(job, personId);
            return Ok();
        }

        [HttpGet("getAllTypes")]
        public IActionResult GetALlNotificationTypes()
        {
            return Ok(_subscriptionService.GetAllNotificationTypes());
        }
    }
}
