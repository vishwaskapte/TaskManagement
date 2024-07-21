using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Repositories
{
    public class SQLStatusRepositories : IStatus
    {
        private readonly TaskManagementDbContext dbContext;

        public SQLStatusRepositories(TaskManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Status>> GetAllAsync()
        {
            return await dbContext.Statuses.ToListAsync();
        }
    }
}
