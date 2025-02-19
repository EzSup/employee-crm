using HRM_Management.Core.AWSS3;
using HRM_Management.Dal;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using static HRM_Management.Tests.Helpers.StorageConfigurationHelper;

namespace HRM_Management.Tests.Dal
{
    public class EmployeeRepositoryTest
    {
        private readonly Mock<IFileStorageRepository> _fileStorageRepository;
        private Mock<AppDbContext> _mockContext;
        private Mock<DbSet<EmployeeEntity>> _mockSet;

        public EmployeeRepositoryTest()
        {
            _fileStorageRepository = new Mock<IFileStorageRepository>();
            _fileStorageRepository.Setup(x => x.GetObjectTempUrlAsync(It.IsAny<string>(), null))
                                  .ReturnsAsync("photoLink");

            var data = GetDefaultTestData();
            ConfigureDbSet(out _mockSet, data);
            ConfigureDbContext(out _mockContext, _mockSet!.Object);
        }

        #region ConfigurationMethods

        private List<EmployeeEntity> GetDefaultTestData()
        {
            var persons = new List<PersonEntity>
            {
                new PersonEntity
                    { Id = 1, FNameEn = "Fname1", LNameEn = "Lname1", MNameEn = "Mname1", Photo = "asdasd" },
                new PersonEntity
                    { Id = 2, FNameEn = "Fname2", LNameEn = "Lname2", MNameEn = "Mname2", Photo = "asdasd" },
                new PersonEntity
                    { Id = 3, FNameEn = "Fname3", LNameEn = "Lname3", MNameEn = "Mname3", Photo = "asdasd" }
            };
            var data = new List<EmployeeEntity>
            {
                new EmployeeEntity { Id = 1, PersonId = 1 },
                new EmployeeEntity { Id = 2, PersonId = 2 },
                new EmployeeEntity { Id = 3, PersonId = 3 }
            };
            foreach (var employee in data)
            {
                employee.Person = persons.First(x => x.Id == employee.PersonId);
            }
            return data;
        }

        #endregion

        #region GetMethods

        [Fact]
        public async Task GetAllAsync_ReturnsAllEmployees()
        {
            //Arrange
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act
            var employeeEntities = await repo.GetAllAsync();
            //Assert
            Assert.Equal(3, employeeEntities.Count());
        }

        [Fact]
        public void GetAllWithJoins_ReturnsAllEmployeesWithJoinedPersons()
        {
            //Arrange
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act
            var result = repo.GetAllWithJoins();
            //Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetAllQueryable_ReturnsAllEmployees()
        {
            //Arrange
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act
            var result = repo.GetAllQueryable();
            //Assert
            Assert.Equal(3, result.Count());
            Assert.Equal(1, result.First().Id);
            Assert.Equal(2, result.Skip(1).First().Id);
            Assert.Equal(3, result.Last().Id);
        }

        [Fact]
        public async Task GetByIdAsync_IdIsInRange_ReturnsEmployeeEntity()
        {
            //Arrange
            var id = 1;
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            var expected = _mockSet.Object.First();
            //Act
            var result = await repo.GetByIdAsync(id);
            //Assert
            Assert.Equal(expected, result);
            Assert.True(result.Person is not null);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        public async Task GetByIdAsync_IdOutOfRange_ThrowsArgumentException(int id)
        {
            //Arrange
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => repo.GetByIdAsync(id));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public async Task GetByIdAsync_ContextContainsNoElementsWithSuchId_ThrowsNullReferenceException(int id)
        {
            //Arrange
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => repo.GetByIdAsync(id));
        }

        #endregion

        #region GetAllWithPhotoLinkAsync

        [Fact]
        public async Task GetAllWithPhotoLinkAsync_AllEmployeesHavePhotos_ReturnsAllEmployees()
        {
            //Arrange
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act
            var result = await repo.GetAllWithDocLinksAsync();
            //Assert
            Assert.Equal(3, result.Count());
            Assert.Equal("photoLink", result.First().Person.Photo);
            _fileStorageRepository.Verify(x => x.GetObjectTempUrlAsync(It.IsAny<string>(), null), Times.Exactly(3));
        }

        [Fact]
        public async Task GetAllWithPhotoLinkAsync_OnlyOneEmployeeHasPhoto_RequestsUrlOnce()
        {
            //Arrange
            var data = GetDefaultTestData();
            data.First(x => x.Id == 2).Person!.Photo = null;
            data.First(x => x.Id == 3).Person!.Photo = null;
            ConfigureDbSet(out _mockSet, data);
            ConfigureDbContext(out _mockContext, _mockSet.Object);
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act
            var result = await repo.GetAllWithDocLinksAsync();
            //Assert
            Assert.Equal(3, result.Count());
            Assert.Equal("photoLink", result.First().Person.Photo!);
            Assert.Equal(null, result.First(x => x.Id == 2).Person.Photo);
            _fileStorageRepository.Verify(x => x.GetObjectTempUrlAsync(It.IsAny<string>(), null), Times.Once);
        }

        [Fact]
        public async Task GetAllWithPhotoLinkAsync_NoneEmployeeHasPhoto_RequestsUrlZeroTimes()
        {
            //Arrange
            var data = GetDefaultTestData();
            foreach (var employee in data)
            {
                if (employee.Person is not null)
                    employee.Person.Photo = null;
            }
            ConfigureDbSet(out _mockSet, data);
            ConfigureDbContext(out _mockContext, _mockSet.Object);
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act
            var result = await repo.GetAllWithDocLinksAsync();
            //Assert
            Assert.Equal(3, result.Count());
            Assert.Equal(new string?[] { null, null, null }, result.Select(x => x.Person.Photo).ToArray());
            _fileStorageRepository.Verify(x => x.GetObjectTempUrlAsync(It.IsAny<string>(), null), Times.Never);
        }

        #endregion

        #region UpdateMethods

        [Fact]
        public async Task UpdateAsync_ValidInput_UpdatAndSaveChangesAsyncCalled()
        {
            //Arrange
            var input = new EmployeeEntity { Id = 1 };
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act
            await repo.UpdateAsync(input);
            //Assert
            _mockSet.Verify(x => x.Update(It.IsAny<EmployeeEntity>()), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_PersonIdIsAlreadyTaken_ThrowsArgumentException()
        {
            //Arrange
            var input = new EmployeeEntity { Id = 1, PersonId = 1 };
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => repo.UpdateAsync(input));
        }

        [Fact]
        public async Task UpdateRangeAsync_UpdateRangeAndSaveChangesAsyncCalled()
        {
            //Arrange
            var input = new List<EmployeeEntity>
            {
                new EmployeeEntity
                    { Id = 1 },
                new EmployeeEntity
                    { Id = 2 }
            };
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act
            await repo.UpdateRangeAsync(input);
            //Assert
            _mockSet.Verify(x => x.UpdateRange(It.IsAny<IEnumerable<EmployeeEntity>>()), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateRangeAsync_TryToUpdateEmployeeWithRepeatedIds_ThrowsArgumentExceltion()
        {
            //Arrange
            var input = new List<EmployeeEntity>
            {
                new EmployeeEntity
                    { Id = 2 },
                new EmployeeEntity
                    { Id = 2 }
            };
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => repo.UpdateRangeAsync(input));
        }

        [Fact]
        public async Task UpdateRangeAsync_TryToReferEmployeeToAlreadyTakenPerson_ThrowsArgumentExceltion()
        {
            //Arrange
            var input = new List<EmployeeEntity>
            {
                new EmployeeEntity
                    { Id = 2, PersonId = 1 },
                new EmployeeEntity
                    { Id = 3, PersonId = 2 }
            };
            var repo = new EmployeeRepository(_mockContext.Object, _fileStorageRepository.Object);
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => repo.UpdateRangeAsync(input));
        }

        #endregion
    }
}
