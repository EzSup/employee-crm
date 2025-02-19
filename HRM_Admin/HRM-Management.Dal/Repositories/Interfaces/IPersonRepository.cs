using HRM_Management.Dal.Entities;
using HRM_Management.Dal.UnitOfWork;

namespace HRM_Management.Dal.Repositories.Interfaces
{
    public interface IPersonRepository : IRepository<PersonEntity>
    {
        Task<PersonEntity?> GetByTelegramIdAsync(long telegramId);
    }
}
