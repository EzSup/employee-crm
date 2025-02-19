using Microsoft.AspNetCore.Http;

namespace HRM_Management.Core.DTOs.BlogDto
{
    public record UpDateBlogRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile ContentFile { get; set; }
    }
}
