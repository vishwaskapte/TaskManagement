using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Data
{
    public class TaskManagementDbContext : DbContext 
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tasks> Taskss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Statuses
            // Staretd, InProgress, Completed

            var statuses = new List<Status>()
            {
                new Status()
                {
                    StatusId = 1,
                    StatusName = "Started",
                },
                new Status()
                {
                    StatusId = 2,
                    StatusName = "InProgress",
                },
                new Status()
                {
                    StatusId = 3,
                    StatusName = "Completed",
                }
            };

            //Seed Statuses to the Database
            modelBuilder.Entity<Status>().HasData(statuses);


            //Seed data for Tasks
            var tasks = new List<Tasks>() {
                new Tasks()
                {
                    TaskId = 1,
                    Title = "First Project",
                    Description = "Firt Project Description",
                    DueDate = DateTime.Now,
                    StatusId = 2
                }
            };

            //Seed Statuses to the Database
            modelBuilder.Entity<Tasks>().HasData(tasks);
        }

    }
}
