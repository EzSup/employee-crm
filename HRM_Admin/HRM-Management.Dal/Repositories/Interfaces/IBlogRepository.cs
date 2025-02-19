using HRM_Management.Dal.Entities;
using HRM_Management.Dal.UnitOfWork;

namespace HRM_Management.Dal.Repositories.Interfaces
{
    public interface IBlogRepository : IRepository<BlogEntity>
    {
        Task<BlogEntity> GetBlogWithCurrentLinkAsync(int id);
    }
}
