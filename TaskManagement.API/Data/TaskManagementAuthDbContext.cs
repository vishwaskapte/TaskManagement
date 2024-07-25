using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.API.Data
{
    public class TaskManagementAuthDbContext : IdentityDbContext
    {
        public TaskManagementAuthDbContext(DbContextOptions<TaskManagementAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                    ConcurrencyStamp = "1"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = "2"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
