using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces.Repositories
{
    public interface ICategoryRepository:IBaseRepository<Category>
    {
        Task<Category> GetByNameAsync(string name);
    }
}
