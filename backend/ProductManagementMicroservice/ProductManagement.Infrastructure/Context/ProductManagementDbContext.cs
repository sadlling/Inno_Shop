using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Context
{
    public class ProductManagementDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options) : base(options) 
        {
            //try
            //{
            //    var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            //    if (dbCreator is not null)
            //    {
            //        if (!dbCreator.CanConnect())
            //        {
            //            dbCreator.Create();
            //        }
            //        if (!dbCreator.HasTables())
            //        {
            //            dbCreator.CreateTables();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //} 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
