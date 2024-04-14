using ProductManagement.Domain.Common;

namespace ProductManagement.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
    }
}
