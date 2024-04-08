using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Context
{
    public class UserManagementDbContext : IdentityDbContext<User>
    {

        public UserManagementDbContext(DbContextOptions options) : base(options)
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

            modelBuilder.Entity<IdentityRole>().HasData(
                [
                   new IdentityRole {Name = "Admin",NormalizedName="Admin", ConcurrencyStamp = Guid.NewGuid().ToString()},
                   new IdentityRole {Name = "User",NormalizedName = "User", ConcurrencyStamp = Guid.NewGuid().ToString()},
                ]);
        }
    }
    
}
