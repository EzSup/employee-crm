using HotChocolate.Authorization;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.UnitOfWork;

namespace HRM_Management.GraphQl.Queries
{
    public partial class GraphQlQuery
    {
        [UsePaging(MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<HubEntity> GetHubs([Service] IUnitOfWork uow)
        {
            var repo = uow.GetRepository<HubEntity>();
            return repo.GetAllQueryable();
        }
    }
}
