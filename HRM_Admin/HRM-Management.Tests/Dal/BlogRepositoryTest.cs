using HRM_Management.Core.AWSS3;
using HRM_Management.Dal;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using static HRM_Management.Tests.Helpers.StorageConfigurationHelper;

namespace HRM_Management.Tests.Dal
{
    public class BlogRepositoryTest
    {
        private readonly Mock<IFileStorageRepository> _fileStorageRepository;
        private readonly Mock<AppDbContext> _mockContext;
        private readonly Mock<DbSet<BlogEntity>> _mockSet;

        public BlogRepositoryTest()
        {
            _fileStorageRepository = new Mock<IFileStorageRepository>();
            _fileStorageRepository.Setup(x => x.GetObjectTempUrlAsync(It.IsAny<string>(), null))
                                  .ReturnsAsync("contentLink");

            var data = GetDefaultTestData();
            ConfigureDbSet(out _mockSet, data);
            _mockSet.Setup(m => m.FindAsync(It.IsAny<object>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return _mockSet.Object.FirstOrDefault(e => e.Id == id);
            });
            ConfigureDbContext(out _mockContext, _mockSet!.Object);
        }

        #region ConfigurationMethods

        private List<BlogEntity> GetDefaultTestData()
        {
            var data = new List<BlogEntity>
            {
                new BlogEntity
                    { Id = 1, Title = "First Blog Title", S3Key = "FirstKey" },
                new BlogEntity
                    { Id = 2, Title = "Second Blog Title", S3Key = "SecondKey" },
                new BlogEntity
                    { Id = 3, Title = "Third Blog Title", S3Key = "ThirdKey" }
            };
            return data;
        }

        #endregion

        [Fact]
        public async Task GetBlogWithCurrentLink_SearchesByIdAndCallsGetObjectTempUrlAsync()
        {
            //Arrange
            var repo = new BlogRepository(_mockContext.Object, _fileStorageRepository.Object);
            var expected = _mockSet.Object.First();
            //Act
            var result = await repo.GetBlogWithCurrentLinkAsync(1);
            //Assert
            Assert.Equal(expected, result);
            _fileStorageRepository.Verify(x => x.GetObjectTempUrlAsync(It.IsAny<string>(), null), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllBlogsWithActualContentLinks()
        {
            //Arrange
            var repo = new BlogRepository(_mockContext.Object, _fileStorageRepository.Object);
            var expected = _mockSet.Object.ToList();
            //Act
            var result = await repo.GetAllAsync();
            //Assert
            Assert.Equal(expected, result);
        }
    }
}
