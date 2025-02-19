using Microsoft.AspNetCore.Http;

namespace HRM_Management.Core.DTOs.BlogDto
{
    public class CreateBlogRequest
    {
        public string Title { get; set; }
        public IFormFile ContentFile { get; set; }
    }
}
