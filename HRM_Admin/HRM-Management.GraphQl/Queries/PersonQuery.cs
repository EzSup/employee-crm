using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;
using Microsoft.AspNetCore.Authorization;

namespace HRM_Management.GraphQl.Queries
{
    public partial class GraphQlQuery
    {
        [UsePaging(MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public async Task<IQueryable<PersonEntity>> GetPersonsAll([Service] IRepository<PersonEntity> personRepository)
        {
            var repo = (IPersonRepository)personRepository.GetAllQueryable();
            return repo.GetAllQueryable();
        }

        [Authorize]
        public async Task<PersonEntity?> GetPersonById([Service] IUnitOfWork unitOfWork, int id)
        {
            var repo = unitOfWork.GetRepository<PersonEntity>();
            return await repo.GetByIdAsync(id);
        }
    }
}
