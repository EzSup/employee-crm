using HRM_TgBot.Core.Models;
using HRM_TgBot.Core.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM_TgBot.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpPost("syncBlogs")]
    public async Task<IActionResult> SyncBlogsWith(List<Blog> blogs)
    {
        await _blogService.HandleBlogSynchronizationAsync(blogs);
        return Ok();
    }
}