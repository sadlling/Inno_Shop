﻿using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Context;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductManagementDbContext _context;
        public ProductRepository(ProductManagementDbContext context) 
        {
            _context = context;
        }
        public async Task CreateAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product entity)
        {
            await _context.Products.
                Where(x=>x.Id == entity.Id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
             return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id==id) ?? null!;
        }

        public async Task UpdateAsync(Product entity)
        {
            await _context.Products
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, entity.Name)
                .SetProperty(p => p.Description, entity.Description)
                .SetProperty(p => p.IsEnabled, entity.IsEnabled)
                .SetProperty(p => p.CategoryId, entity.CategoryId)
                .SetProperty(p=>p.UpdatedAt, DateTimeOffset.Now)
                );
        }
    }
}
