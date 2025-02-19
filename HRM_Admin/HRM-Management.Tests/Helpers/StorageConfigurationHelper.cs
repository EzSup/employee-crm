using HRM_Management.Dal;
using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
namespace HRM_Management.Tests.Helpers
{
    public static class StorageConfigurationHelper
    {
        public static void ConfigureDbSet<T>(out Mock<DbSet<T>> mockSet, List<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var dataEnumerator = new TestDbAsyncEnumerator<T>(data.GetEnumerator());

            mockSet = new Mock<DbSet<T>>();
            mockSet.As<IAsyncEnumerable<T>>()
                   .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                   .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));
            mockSet.As<IQueryable<T>>()
                   .Setup(m => m.Provider)
                   .Returns(new TestDbAsyncQueryProvider<T>(queryable.Provider));
            mockSet.As<IQueryable<T>>()
                   .Setup(m => m.Expression)
                   .Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>()
                   .Setup(m => m.ElementType)
                   .Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>()
                   .Setup(m => m.GetEnumerator())
                   .Returns(() => queryable.GetEnumerator());
        }

        public static void ConfigureDbContext<T>(out Mock<AppDbContext> mockContext, DbSet<T> dbSet) where T : class
        {
            mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Set<T>()).Returns(dbSet);
        }
    }
}
