using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Dal;
using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using static HRM_Management.Core.Helpers.DtoPropertiesHelper;

namespace HRM_Management.GraphQl.Resolvers
{

    namespace HRM_Management.GraphQl.Resolvers
    {
        public class HubResolvers
        {
            public async Task<int> GetEmployeesCountAsync([Service] IDbContextFactory<AppDbContext> factory, [Parent] HubEntity hubEntity)
            {
                await using (var dbContext = await factory.CreateDbContextAsync())
                {
                    var result = await dbContext.Employees.CountAsync(x => x.HubId == hubEntity.Id);
                    return result;
                }
            }

            public async Task<string?> GetDirectorNameAsync([Service] IDbContextFactory<AppDbContext> factory, [Parent] HubEntity hub)
            {
                await using (var dbContext = await factory.CreateDbContextAsync())
                {
                    var fullName = await dbContext.Set<HubEntity>()
                                       .Include(e => e.Director)
                                       .ThenInclude(emp => emp.Person)
                                       .Where(e => e.Id == hub.Id)
                                       .Select(e => UniteFullName(e.Director.Person.FNameEn, e.Director.Person.LNameEn, e.Director.Person.MNameEn,
                                                                  NameFormat.English))
                                       .FirstOrDefaultAsync();

                    return string.IsNullOrWhiteSpace(fullName) ? null : fullName;
                }
            }

            public async Task<string?> GetLeaderNameAsync([Service] IDbContextFactory<AppDbContext> factory, [Parent] HubEntity hub)
            {
                await using (var dbContext = await factory.CreateDbContextAsync())
                {
                    var fullName = await dbContext.Set<HubEntity>()
                                       .Include(e => e.Leader)
                                       .ThenInclude(emp => emp.Person)
                                       .Where(e => e.Id == hub.Id)
                                       .Select(e => UniteFullName(e.Leader.Person.FNameEn, e.Leader.Person.LNameEn, e.Leader.Person.MNameEn,
                                                                  NameFormat.English))
                                       .FirstOrDefaultAsync();

                    return string.IsNullOrWhiteSpace(fullName) ? null : fullName;
                }
            }

            public async Task<string?> GetDeputyLeaderNameAsync([Service] IDbContextFactory<AppDbContext> factory, [Parent] HubEntity hub)
            {
                await using (var dbContext = await factory.CreateDbContextAsync())
                {
                    var fullName = await dbContext.Set<HubEntity>()
                                       .Include(e => e.DeputyLeader)
                                       .ThenInclude(emp => emp.Person)
                                       .Where(e => e.Id == hub.Id)
                                       .Select(e => UniteFullName(e.DeputyLeader.Person.FNameEn, e.DeputyLeader.Person.LNameEn,
                                                                  e.DeputyLeader.Person.MNameEn,
                                                                  NameFormat.English))
                                       .FirstOrDefaultAsync();

                    return string.IsNullOrWhiteSpace(fullName) ? null : fullName;
                }
            }
        }
    }
}
