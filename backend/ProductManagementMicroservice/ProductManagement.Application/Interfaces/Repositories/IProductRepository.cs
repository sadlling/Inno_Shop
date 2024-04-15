using ProductManagement.Application.Common.Paging;
using ProductManagement.Domain.Entities;
using System.Linq.Expressions;

namespace ProductManagement.Application.Interfaces.Repositories
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<Product> FindByNameAsync(string Name);
        Task<List<Product>> FindByConditionAsync(Expression<Func<Product, bool>> expression);
    }
}
