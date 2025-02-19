namespace HRM_Management.Core.DTOs.BlogDto
{
    public record GetBlogResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? ContentLink { get; set; }
    }
}
