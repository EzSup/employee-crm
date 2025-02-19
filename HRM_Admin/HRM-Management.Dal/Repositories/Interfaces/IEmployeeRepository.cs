using HRM_Management.Dal.Entities;
using HRM_Management.Dal.UnitOfWork;

namespace HRM_Management.Dal.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<EmployeeEntity>
    {
        Task UpdateRangeAsync(List<EmployeeEntity> entities);
        Task<IEnumerable<EmployeeEntity>> GetAllWithDocLinksAsync();
    }
}
