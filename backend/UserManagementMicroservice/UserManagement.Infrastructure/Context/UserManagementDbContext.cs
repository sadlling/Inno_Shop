using Microsoft.EntityFrameworkCore;

namespace UserManagement.Infrastructure.Context
{
    public class UserManagementDbContext : DbContext
    {
        public UserManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
