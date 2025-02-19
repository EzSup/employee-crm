using AutoMapper;
using HRM_Management.Bll.Helpers;
using HRM_Management.Bll.Services;
using HRM_Management.Core.AWSS3;
using HRM_Management.Core.DTOs.BlogDto;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;
using HRM_Management.DalS3.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using static HRM_Management.Tests.Helpers.StorageConfigurationHelper;

namespace HRM_Management.Tests.Bll;

public class BlogServiceTest
{
    private readonly IMapper _mapper;

    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<DbSet<BlogEntity>> _mockSet;
    private readonly Mock<IBlogRepository> _blogRepository = new();

    private readonly Mock<IOptions<S3Options>> _mockS3Options = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();
    private readonly Mock<IFileStorageRepository> _fileRepository = new();
    private readonly Mock<ILogger<BlogService>> _mockLogger = new();

    private BlogService _blogService;
    private Mock<HttpMessageHandler> _handlerMock;
    private HttpClient _httpClient;

    public BlogServiceTest()
    {
        var mapperProfiles = new AppMappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfiles));
        _mapper = new Mapper(configuration);
        ConfigureDbSet(out _mockSet, GetBlogData());

        _mockS3Options.Setup(opt => opt.Value).Returns(new S3Options { S3BlogFolder = "Blogs" });

        _blogRepository.Setup(repo => repo.GetBlogWithCurrentLinkAsync(It.IsAny<int>()))!
            .ReturnsAsync(_mockSet.Object.First());

        _blogRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(_mockSet.Object.First());

        _blogRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(_mockSet.Object.ToList());

        _blogRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BlogEntity>()))
            .Callback<BlogEntity>(b =>
            {
                var existingEntity = _mockSet.Object.FirstOrDefault(x => x.Id == b.Id);

                if (existingEntity != null)
                {
                    existingEntity.ContentLink = b.ContentLink;
                    existingEntity.S3Key = b.S3Key;
                    existingEntity.Title = b.Title;
                }
            })
            .Returns(Task.CompletedTask);

        _blogRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        _fileRepository.Setup(repo => repo.GetObjectTempUrlAsync(It.IsAny<string>(), null))
            .ReturnsAsync("FileTempUrl");

        _fileRepository.Setup(repo => repo.DeleteFileAsync(It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        _fileRepository.Setup(repo => repo.UploadFileAsync(
                    It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync("testBlogKey");

        _unitOfWork.Setup(uow => uow.GetRepository<BlogEntity>(true))
            .Returns(_blogRepository.Object);

        _httpClient = GetHttpClientAndConfigureHandler(
           "http://testuri/api/",
           "http://testuri/api/Blog/syncBlogs"
           , HttpMethod.Post,
           HttpStatusCode.OK,
           handlerMock: out _handlerMock);

        _httpClientFactory.Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(_httpClient);

        _blogService =
           new BlogService(_unitOfWork.Object, _fileRepository.Object, _mapper,
               _mockS3Options.Object, _httpClientFactory.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsBlogIfExists()
    {
        //Arrange 
        var blogService =
            new BlogService(_unitOfWork.Object, _fileRepository.Object, _mapper,
                _mockS3Options.Object, _httpClientFactory.Object, _mockLogger.Object);
        var expectedBlog = _mockSet.Object.First();
        //Act
        var result = await blogService.GetByIdAsync(expectedBlog.Id);
        //Asser 
        Assert.NotNull(result);
        Assert.Equal(expectedBlog.ContentLink, result.ContentLink);
        Assert.Equal(expectedBlog.Title, result.Title);
        _blogRepository.Verify(repo => repo.GetBlogWithCurrentLinkAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task GetAllBlogsAsync_ReturnsAllBlogs()
    {
        //Arrange
        var blogService =
            new BlogService(_unitOfWork.Object, _fileRepository.Object, _mapper,
                _mockS3Options.Object, _httpClientFactory.Object, _mockLogger.Object);
        var expectedBlogs = _mockSet.Object.ToList();
        //Act
        var result = await blogService.GetAllBlogsAsync();
        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedBlogs.Count, result.Count);
        Assert.Equal(expectedBlogs[1].Title, result[1].Title);
        Assert.Equal(expectedBlogs[0].Id, result[0].Id);
    }

    [Fact]
    public async Task DeleteBlogAsync_RemovesBlogRecordAndAssociatedFile()
    {
        //Act
        var result = _blogService.DeleteAsync(1);
        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsCompletedSuccessfully);
        _handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post &&
                req.RequestUri == new Uri("http://testUri/api/Blog/syncBlogs")
            ),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task AddBlogWithFileAsync_UploadsFileToS3AddsNewBlogRecordSyncBlogs()
    {
        //Arrange  
        var createBlogRequest = GetCreateBlogRequest();
        //Act
        var result = _blogService.AddBlogWithFileAsync(createBlogRequest);
        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsCompletedSuccessfully);
        _handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post &&
                req.RequestUri == new Uri("http://testuri/api/Blog/syncBlogs")
            ),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task UpDateBlogWithFileAsync_UpdatesBlogAndBlogFile_SyncBlogs()
    {
        //Arrange

        var updateBlockRequest = GetUpDateBlogRequest();
        //Act
        var result = _blogService.UpDateBlogWithFileAsync(updateBlockRequest);
        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsCompletedSuccessfully);
        Assert.Equal("testBlogKey", _mockSet.Object.First().S3Key);
        Assert.Equal("FileTempUrl", _mockSet.Object.First().ContentLink);
        Assert.Equal("Updated First Blog ", _mockSet.Object.First().Title);
        _blogRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _fileRepository.Verify(repo => repo.GetObjectTempUrlAsync(It.IsAny<string>(), null), Times.Once);
        _blogRepository.Verify(repo => repo.UpdateAsync(It.IsAny<BlogEntity>()), Times.Once);
        _handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post &&
                String.Equals("http://testuri/api/Blog/syncBlogs", req.RequestUri.ToString())
            ),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task SyncBlogsAsync_SynchronizesBlogsWithExternalService()
    {
        // Arrange
        _httpClient = GetHttpClientAndConfigureHandler(
            "http://testuri/api/",
            "http://testuri/api/Blog/syncBlogs"
            , HttpMethod.Post,
            HttpStatusCode.InternalServerError,
            handlerMock: out _handlerMock);

        _httpClientFactory.Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(_httpClient);

        _blogService =
            new BlogService(_unitOfWork.Object, _fileRepository.Object, _mapper,
                _mockS3Options.Object, _httpClientFactory.Object, _mockLogger.Object);
        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => _blogService.SyncBlogs());

        Assert.Contains("Error syncing blogs", exception.Message);

        _handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post &&
                req.RequestUri.ToString() == "http://testuri/api/Blog/syncBlogs"
            ),
            ItExpr.IsAny<CancellationToken>());
    }

    #region ConfigurationMethods

    private HttpClient GetHttpClientAndConfigureHandler(
        string baseUrl,
        string expectedRequestUri,
        HttpMethod method,
        HttpStatusCode httpStatusCode,
        out Mock<HttpMessageHandler> handlerMock)
    {
        handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == method &&
                    req.RequestUri.ToString() == expectedRequestUri),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = httpStatusCode,
            }).Verifiable();

        var httpClient = new HttpClient(handlerMock.Object);
        httpClient.BaseAddress = new Uri(baseUrl);
        return httpClient;
    }

    private List<BlogEntity> GetBlogData()
    {
        return new List<BlogEntity>
        {
            new BlogEntity { Id = 1, Title = "First Blog", ContentLink = "https://example.com/blog1", S3Key = "s3-key-1" },
            new BlogEntity { Id = 2, Title = "Second Blog", ContentLink = "https://example.com/blog2", S3Key = "s3-key-2" },
            new BlogEntity { Id = 3, Title = "Third Blog", ContentLink = "https://example.com/blog3", S3Key = "s3-key-3" }
        };
    }

    private CreateBlogRequest GetCreateBlogRequest()
    {
        var stream = new MemoryStream();
        using (var writer = new StreamWriter(stream, leaveOpen: true))
        {
            writer.Write("%PDF-1.4 This is a fake PDF content");
            writer.Flush();
        }
        stream.Position = 0;

        var formFile = new FormFile(stream, 0, stream.Length, "file", "test.pdf")
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/pdf"
        };
        return new CreateBlogRequest()
        {
            Title = "New Blog",
            ContentFile = formFile
        };
    }

    private UpDateBlogRequest GetUpDateBlogRequest()
    {
        var stream = new MemoryStream();
        using (var writer = new StreamWriter(stream, leaveOpen: true))
        {
            writer.Write("%PDF-1.4 This is a fake PDF content");
            writer.Flush();
        }
        stream.Position = 0;

        var formFile = new FormFile(stream, 0, stream.Length, "file", "test.pdf")
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/pdf"
        };
        return new UpDateBlogRequest()
        {
            Id = 1,
            Title = "Updated First Blog ",
            ContentFile = formFile
        };
    }

    #endregion

}