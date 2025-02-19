using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HRM_TgBot.DAL.Repository.UnitOfWork
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet
                .ToListAsync();
        }

        public virtual async Task<TEntity>? GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(long id)
        {
            // I changed the realization because executedeleteasync throws exception if i call EF.Property in query
            var entity = await _dbSet
                .Where(entity => EF.Property<long>(entity, "UserTgID") == id)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new InvalidOperationException($"No entity found with ID {id} to delete.");

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
