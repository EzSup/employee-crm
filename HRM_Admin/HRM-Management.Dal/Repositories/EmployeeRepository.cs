using HRM_Management.Core.AWSS3;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;
using Microsoft.EntityFrameworkCore;
namespace HRM_Management.Dal.Repositories
{
    public class EmployeeRepository : Repository<EmployeeEntity>, IEmployeeRepository
    {
        private readonly IFileStorageRepository _fileStorageRepository;

        public EmployeeRepository(AppDbContext context, IFileStorageRepository fileStorageRepository
        ) : base(context)
        {
            _fileStorageRepository = fileStorageRepository;
        }

        public override async Task<EmployeeEntity> GetByIdAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException("Invalid Id!");

            return await _dbSet
                         .Include(e => e.Person)
                         .Include(e => e.Hub)
                         .Include(e => e.ExEmployee)
                         .FirstOrDefaultAsync(e => e.Id == id)
                   ?? throw new NullReferenceException($"Entity with ID = {id} not found.");
        }

        public override async Task UpdateAsync(EmployeeEntity entity)
        {
            if (await _dbSet.AnyAsync(x => x.PersonId == entity.PersonId))
                throw new ArgumentException($"Entity with PersonId = {entity.PersonId} already exists.");
            await base.UpdateAsync(entity);
        }

        public async Task UpdateRangeAsync(List<EmployeeEntity> entities)
        {
            if (entities.GroupBy(x => x.Id)
                        .Any(g => g.Count() > 1))
                throw new ArgumentException("Id has to be unique for each employee!");
            if (await _dbSet
                    .AnyAsync(dbItem => entities
                                  .Any(e => e.PersonId == dbItem.PersonId
                                            && e.Id != dbItem.Id)))
                throw new ArgumentException("Person Id has to be unique for each employee!");
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public new IQueryable<EmployeeEntity> GetAllQueryable()
        {
            return _dbSet;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllWithDocLinksAsync()
        {
            var employees = GetAllWithJoins().ToList();
            foreach (var employee in employees.Where(e => e.Person is not null && !string.IsNullOrWhiteSpace(e.Person?.Photo)))
            {
                employee.Person!.Photo = await _fileStorageRepository.GetObjectTempUrlAsync(employee.Person.Photo!);
            }

            return employees;
        }

        public IQueryable<EmployeeEntity> GetAllWithJoins()
        {
            return GetAllQueryable()
                   .Include(e => e.Hub)
                   .Include(e => e.Hirer)
                   .Include(e => e.Person);
        }
    }
}
