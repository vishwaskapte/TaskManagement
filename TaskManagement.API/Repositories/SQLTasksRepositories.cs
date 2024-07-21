using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Domain;
using TaskManagement.API.Models.DTO;

namespace TaskManagement.API.Repositories
{
    public class SQLTasksRepositories : ITasks
    {
        private readonly TaskManagementDbContext dbContext;

        public SQLTasksRepositories(TaskManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Tasks> CreateAsync(Tasks task)
        {
            await dbContext.AddAsync(task);
            await dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<Tasks?> DeleteAsync(int id)
        {
            var existingTask = await dbContext.Taskss.FirstOrDefaultAsync(x => x.TaskId == id);

            if(existingTask == null)
            {
                return null;
            }

            dbContext.Taskss.Remove(existingTask);
            await dbContext.SaveChangesAsync();
            return existingTask;
        }

        public async Task<List<Tasks>> GetAllAsync()
        {
            return await dbContext.Taskss.Include("Status").ToListAsync();
        }

        public async Task<Tasks?> GetByIdAsync(int id)
        {
            return await dbContext.Taskss.FirstOrDefaultAsync(x => x.TaskId == id);
        }

        public async Task<Tasks?> UpdateAsync(int id, Tasks task)
        {
            //check if task exist
            var existingTasks = await dbContext.Taskss.FirstOrDefaultAsync(x => x.TaskId == id);

            if (existingTasks == null)
            {
                return null;
            }

            //map DTO to Domain Model
            existingTasks.Title = task.Title;
            existingTasks.Description = task.Description;
            existingTasks.DueDate = task.DueDate;
            existingTasks.StatusId = task.StatusId;

            await dbContext.SaveChangesAsync();
            return existingTasks;
        }
    }
}
