using HRM_Management.Core.AWSS3;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;
namespace HRM_Management.Dal.Repositories
{
    public class BlogRepository : Repository<BlogEntity>, IBlogRepository
    {
        private readonly IFileStorageRepository _fileStorageRepository;

        public BlogRepository(AppDbContext context, IFileStorageRepository fileStorageRepository) : base(context)
        {
            _fileStorageRepository = fileStorageRepository;
        }

        public override async Task<IEnumerable<BlogEntity>> GetAllAsync()
        {
            var userIds = base.GetAllAsync().Result.Select(blog => blog.Id).ToList();
            var blogs = new List<BlogEntity>();
            foreach (var blog in userIds)
            {
                blogs.Add(await GetBlogWithCurrentLinkAsync(blog));
            }
            return blogs;
        }

        public async Task<BlogEntity> GetBlogWithCurrentLinkAsync(int id)
        {
            var blog = await base.GetByIdAsync(id);
            blog.ContentLink = await _fileStorageRepository.GetObjectTempUrlAsync(blog.S3Key);
            return blog;
        }
    }
}
