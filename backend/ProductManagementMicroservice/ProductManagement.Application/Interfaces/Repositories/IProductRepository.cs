﻿using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces.Repositories
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<Product> FindByNameAsync(string Name);
    }
}
