using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;
using Microsoft.EntityFrameworkCore;
namespace HRM_Management.Dal.Repositories
{
    public class PersonRepository : Repository<PersonEntity>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context) { }

        public override async Task<PersonEntity> GetByIdAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException("Invalid Id!");

            return await _dbSet
                         .Include(x => x.Partner)
                         .Include(x => x.Children)
                         .FirstOrDefaultAsync(x => x.Id == id)
                   ?? throw new NullReferenceException($"Entity with ID = {id} not found.");
        }

        public async Task<PersonEntity?> GetByTelegramIdAsync(long telegramId)
        {
            if (telegramId < 1)
                throw new NullReferenceException("Telegram id can`t be negative number!.");
            var person = await _dbSet.FirstOrDefaultAsync(p => p.TelegramId == telegramId);
            return person;
        }
    }
}
