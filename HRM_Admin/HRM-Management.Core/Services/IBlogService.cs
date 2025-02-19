using HRM_Management.Core.DTOs.BlogDto;

namespace HRM_Management.Core.Services
{
    public interface IBlogService
    {
        Task AddBlogWithFileAsync(CreateBlogRequest blog);
        Task UpDateBlogWithFileAsync(UpDateBlogRequest blogDto);
        Task<List<GetBlogResponse>> GetAllBlogsAsync();
        Task<GetBlogResponse> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task SyncBlogs();
    }
}
