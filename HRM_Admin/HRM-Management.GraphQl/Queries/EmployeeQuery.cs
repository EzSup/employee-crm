using HotChocolate.Authorization;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;

namespace HRM_Management.GraphQl.Queries
{
    public partial class GraphQlQuery
    {
        [UsePaging(DefaultPageSize = 100, MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public async Task<IQueryable<EmployeeEntity>> GetEmployees([Service] IRepository<EmployeeEntity> employeeRepository)
        {
            var repo = (IEmployeeRepository)employeeRepository;

            return repo.GetAllQueryable();
        }
    }
}
