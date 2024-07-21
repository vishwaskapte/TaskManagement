using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Repositories
{
    public interface IStatus
    {
        Task<List<Status>> GetAllAsync();
    }
}
