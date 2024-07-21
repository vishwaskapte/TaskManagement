using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models.Domain;
using TaskManagement.API.Models.DTO;

namespace TaskManagement.API.Repositories
{
    public interface ITasks
    {
        Task<List<Tasks>> GetAllAsync();
        Task<Tasks?> GetByIdAsync(int id);
        Task<Tasks> CreateAsync(Tasks task);
        Task<Tasks?> UpdateAsync(int id, Tasks task);
        Task<Tasks?> DeleteAsync(int id);
    }
}
