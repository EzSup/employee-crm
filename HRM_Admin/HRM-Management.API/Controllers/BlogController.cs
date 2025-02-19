using HRM_Management.Core.DTOs.BlogDto;
using HRM_Management.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Management.API.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController(IBlogService blogService) : ControllerBase
    {
        private readonly IBlogService _blogService = blogService;

        [HttpPost]
        public async Task<IActionResult> AddNewBlog(CreateBlogRequest request)
        {
            await _blogService.AddBlogWithFileAsync(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpDateBlog(UpDateBlogRequest dateBlogRequest)
        {
            await _blogService.UpDateBlogWithFileAsync(dateBlogRequest);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogByID(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            return Ok(blog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogById(int id)
        {
            await _blogService.DeleteAsync(id);
            return Ok();
        }
    }
}
