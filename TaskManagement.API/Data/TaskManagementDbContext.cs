using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Data
{
    public class TaskManagementDbContext : DbContext 
    {
        public TaskManagementDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tasks> Taskss { get; set; }
        
    }
}
