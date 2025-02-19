using AutoMapper;
using HRM_Management.Core.AWSS3;
using HRM_Management.Core.DTOs.BlogDto;
using HRM_Management.Core.Services;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;
using HRM_Management.DalS3.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using static HRM_Management.Core.Helpers.Constants;
using static HRM_Management.Core.Helpers.HelperMethods;
namespace HRM_Management.Bll.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IFileStorageRepository _fileStorage;
        private readonly IMapper _mapper;
        private readonly S3Options _s3Options;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BlogService> _logger;

        public BlogService(IUnitOfWork unitOfWork, IFileStorageRepository fileStorage, IMapper mapper,
            IOptions<S3Options> s3Options, IHttpClientFactory httpClientFactory, ILogger<BlogService> logger)
        {
            _httpClient = httpClientFactory.CreateClient(TgBotHttpClientName);
            _blogRepository = (IBlogRepository)unitOfWork.GetRepository<BlogEntity>();
            _fileStorage = fileStorage;
            _mapper = mapper;
            _s3Options = s3Options.Value;
            _logger = logger;
        }

        public async Task AddBlogWithFileAsync(CreateBlogRequest blog)
        {
            var newBlog = _mapper.Map<BlogEntity>(blog);
            newBlog.S3Key = await _fileStorage.UploadFileAsync(blog.ContentFile, _s3Options.S3BlogFolder);

            await _blogRepository.AddAsync(newBlog);
            await SyncBlogs();
        }

        public async Task UpDateBlogWithFileAsync(UpDateBlogRequest blogDto)
        {
            var blog = _mapper.Map<BlogEntity>(blogDto);

            var blogToUpdate = await _blogRepository.GetByIdAsync(blogDto.Id);

            blog.S3Key = await _fileStorage.UploadFileAsync(blogDto.ContentFile, blogToUpdate.S3Key);
            blog.ContentLink = await _fileStorage.GetObjectTempUrlAsync(blog.S3Key);

            await _blogRepository.UpdateAsync(blog);
            await SyncBlogs();
        }

        public async Task<GetBlogResponse> GetByIdAsync(int id)
        {
            return _mapper.Map<GetBlogResponse>(await _blogRepository.GetBlogWithCurrentLinkAsync(id));
        }

        public async Task<List<GetBlogResponse>> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllAsync();
            return _mapper.Map<List<GetBlogResponse>>(blogs);
        }

        public async Task DeleteAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            await _fileStorage.DeleteFileAsync(blog.S3Key);
            await _blogRepository.DeleteAsync(id);
            await SyncBlogs();
        }

        public async Task SyncBlogs()
        {
            var blogs = await _blogRepository.GetAllAsync();
            var response = await _httpClient.PostAsync("Blog/syncBlogs", CreateJsonContent(blogs, Encoding.UTF8));

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to sync blogs. Status Code: {response.StatusCode}. Response: {errorMessage}");
                throw new HttpRequestException($"Error syncing blogs: {response.StatusCode}");
            }
        }
    }
}