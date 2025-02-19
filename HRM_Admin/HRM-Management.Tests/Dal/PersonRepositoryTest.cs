using HRM_Management.Dal;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using static HRM_Management.Tests.Helpers.StorageConfigurationHelper;

namespace HRM_Management.Tests.Dal
{
    public class PersonRepositoryTest
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly Mock<DbSet<PersonEntity>> _mockSet;

        public PersonRepositoryTest()
        {
            var data = GetDefaultTestData();
            ConfigureDbSet(out _mockSet, data);
            ConfigureDbContext(out _mockContext, _mockSet!.Object);
        }

        #region ConfigurationMethods

        private List<PersonEntity> GetDefaultTestData()
        {
            var data = new List<PersonEntity>
            {
                new PersonEntity
                    { Id = 1, FNameEn = "Fname1", LNameEn = "Lname1", MNameEn = "Mname1", Photo = "asdasd", TelegramId = 5565 },
                new PersonEntity
                    { Id = 2, FNameEn = "Fname2", LNameEn = "Lname2", MNameEn = "Mname2", Photo = "asdasd" },
                new PersonEntity
                    { Id = 3, FNameEn = "Fname3", LNameEn = "Lname3", MNameEn = "Mname3", Photo = "asdasd", TelegramId = 1 }
            };
            return data;
        }

        #endregion

        #region GetMethods

        [Fact]
        public async Task GetByIdAsync_IdIsInRange_ReturnsEmployeeEntity()
        {
            //Arrange
            var id = 1;
            var repo = new PersonRepository(_mockContext.Object);
            var expected = _mockSet.Object.First();
            //Act
            var result = await repo.GetByIdAsync(id);
            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        public async Task GetByIdAsync_IdOutOfRange_ThrowsArgumentException(int id)
        {
            //Arrange
            var repo = new PersonRepository(_mockContext.Object);
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => repo.GetByIdAsync(id));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public async Task GetByIdAsync_ContextContainsNoElementsWithSuchId_ThrowsNullReferenceException(int id)
        {
            //Arrange
            var repo = new PersonRepository(_mockContext.Object);
            //Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => repo.GetByIdAsync(id));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5565)]
        public async Task GetByTelegramIdAsync_IdIsInRange_ReturnsEmployeeEntity(int id)
        {
            //Arrange
            var repo = new PersonRepository(_mockContext.Object);
            var expected = _mockSet.Object.First(x => x.TelegramId == id);
            //Act
            var result = await repo.GetByTelegramIdAsync(id);
            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(long.MaxValue)]
        [InlineData(1488)]
        public async Task GetByTelegramIdAsync_IdIsValidButNotInDataContext_ReturnsNullObject(long id)
        {
            //Arrange
            PersonEntity? expected = null;
            var repo = new PersonRepository(_mockContext.Object);
            //Act
            var result = await repo.GetByTelegramIdAsync(id);
            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(long.MinValue)]
        public async Task GetByTelegramIdAsync_IdIsOutOfRange_ThrowsNullReferenceException(long id)
        {
            //Arrange
            var repo = new PersonRepository(_mockContext.Object);
            //Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => repo.GetByTelegramIdAsync(id));
        }

        #endregion
    }
}
