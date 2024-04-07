using UserManagement.Domain.Common;

namespace UserManagement.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task<T> GetByIdAsync(Guid id);
        public Task<T> GetAllAsync(string name);
    }
}
