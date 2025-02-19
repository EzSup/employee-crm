using HRM_TgBot.Core.DTOs;
using HRM_TgBot.Core.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM_TgBot.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost("sendMessageToUser")]
    public async Task<IActionResult> SendMessageToManyUsers([FromBody] SendMessageDtoRequest messageDto)
    {
        try
        {
            foreach (var chat in messageDto.ChatIds)
                await _messageService.SendTextMessageAsync(messageDto.Message, chat);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}