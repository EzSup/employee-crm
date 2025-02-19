using HRM_Management.Dal;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Entities.Enums;
using HRM_Management.Dal.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using HRM_Management.Core.Helpers.Exceptions;
using static HRM_Management.Tests.Helpers.StorageConfigurationHelper;

namespace HRM_Management.Tests.Dal
{
    public class GenericRepositoryTest
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly Mock<DbSet<PartnerEntity>> _mockSet;

        public GenericRepositoryTest()
        {
            var data = GetDefaultTestData();
            ConfigureDbSet(out _mockSet, data);
            // REVIEW: is there is any sense of testing the method GetByIdAsync, if it`s main called function I mock?
            _mockSet.Setup(m => m.FindAsync(It.IsAny<object>())).ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids.First();
                return _mockSet.Object.FirstOrDefault(e => e.Id == id);
            });
            ConfigureDbContext(out _mockContext, _mockSet!.Object);
        }

        #region ConfigurationMethods

        private List<PartnerEntity> GetDefaultTestData()
        {
            var data = new List<PartnerEntity>
            {
                new PartnerEntity
                    { Id = 1, Name = "John Doe", BirthDate = DateTime.Now.AddYears(-20), Gender = Gender.Male },
                new PartnerEntity
                    { Id = 2, Name = "Maria Brown", BirthDate = DateTime.Now.AddYears(-30), Gender = Gender.Female },
                new PartnerEntity
                    { Id = 3, Name = "Joanna Jenkins", BirthDate = DateTime.Now.AddYears(-23), Gender = Gender.Female }
            };
            return data;
        }

        #endregion

        #region GetMethods

        [Fact]
        public async Task GetByIdAsync_FirstObjectIdGiven_ReturnsEntity()
        {
            //Arrange
            var repo = new Repository<PartnerEntity>(_mockContext.Object);
            var expected = _mockSet.Object.First();
            //Act
            var result = await repo.GetByIdAsync(1);
            //Assert
            Assert.Equal(expected, result);
            _mockSet.Verify(m => m.FindAsync(It.IsAny<object>()), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        public async Task GetByIdAsync_IdOutOfRange_ThrowsArgumentException(int id)
        {
            //Arrange
            var repo = new Repository<PartnerEntity>(_mockContext.Object);
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => repo.GetByIdAsync(id));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public async Task GetByIdAsync_ContextContainsNoElementsWithSuchId_ThrowsNullReferenceException(int id)
        {
            //Arrange
            var repo = new Repository<PartnerEntity>(_mockContext.Object);
            //Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => repo.GetByIdAsync(id));
        }

        [Fact]
        public void GetAllAsync_ReturnsDbSetAsList()
        {
            //Arrange
            var repo = new Repository<PartnerEntity>(_mockContext.Object);
            var expected = _mockSet.Object.ToList();
            //Act
            var result = repo.GetAllQueryable();
            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAllQueryable_ReturnsDbSetAsQueryable()
        {
            //Arrange
            var repo = new Repository<PartnerEntity>(_mockContext.Object);
            var expected = _mockSet.Object;
            //Act
            var result = repo.GetAllQueryable();
            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetWhereAsync_ReturnsDbSetRecordsByCondition()
        {
            //Arrange
            Expression<Func<PartnerEntity, bool>> expression = partner => partner.Id > 0;
            var repo = new Repository<PartnerEntity>(_mockContext.Object);
            var expected = _mockSet.Object.ToList();
            //Act
            var result = await repo.GetWhereAsync(expression);
            //Assert
            Assert.Equal(expected, result);
        }

        // REVIEW: Delete method was not tested because it uses EF static class to get primary key column. It is impossible to mock

        #endregion

        #region CreateAndUpdateMethods

        [Fact]
        public async Task AddAsync_ValidInput_AddAsyncAndSaveChangesAsyncCalled()
        {
            //Arrange
            var input = new PartnerEntity
                { Name = "New Partner" };
            var repo = new Repository<PartnerEntity>(_mockContext.Object);
            //Act
            await repo.AddAsync(input);
            //Assert
            _mockSet.Verify(x => x.AddAsync(It.IsAny<PartnerEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddRangeAsync_ValidInput_AddRangeAsyncAndSaveChangesAsyncCalled()
        {
            //Arrange
            var input = new List<PartnerEntity>
            {
                new PartnerEntity
                    { Name = "New Partner" },
                new PartnerEntity
                    { Name = "New Partner" }
            };
            var repo = new Repository<PartnerEntity>(_mockContext.Object);
            //Act
            await repo.AddRangeAsync(input);
            //Assert
            _mockSet.Verify(x => x.AddRangeAsync(It.IsAny<IEnumerable<PartnerEntity>>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ValidInput_UpdateAndSaveChangesAsyncCalled()
        {
            //Arrange
            var input = new PartnerEntity
                { Id = 1, Name = "New Partner" };
            var repo = new Repository<PartnerEntity>(_mockContext.Object);
            //Act
            await repo.UpdateAsync(input);
            //Assert
            _mockSet.Verify(x => x.Update(It.IsAny<PartnerEntity>()), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        #endregion
    }
}
