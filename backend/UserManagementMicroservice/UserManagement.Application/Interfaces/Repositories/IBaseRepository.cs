using UserManagement.Domain.Common;

namespace UserManagement.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task<T> GetByIdAsync(string id);
        public Task<List<T>> GetAllAsync();
    }
}
