using ImagineApps.Domain.Entities;

namespace ImagineApps.Infrastructure.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
