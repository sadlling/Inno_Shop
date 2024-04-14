using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Context;

namespace ProductManagement.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductManagementDbContext _context;
        public CategoryRepository(ProductManagementDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category entity)
        {
            await _context.Categories.
               Where(x => x.Id == entity.Id)
               .ExecuteDeleteAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) ?? null!;
        }

        public async Task UpdateAsync(Category entity)
        {
            await _context.Categories
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, entity.Name)
                .SetProperty(p => p.Description, entity.Description)
                .SetProperty(p => p.UpdatedAt, DateTimeOffset.Now)
                );
        }
    }
}
