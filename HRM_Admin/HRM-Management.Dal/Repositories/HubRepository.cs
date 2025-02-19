using HRM_Management.Dal.Entities;
using HRM_Management.Dal.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HRM_Management.Dal.Repositories
{
    public class HubRepository : Repository<HubEntity>
    {
        public HubRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(int id)
        {
            await _dbSet.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
