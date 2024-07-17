using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.Models.Domain
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string? StatusName { get; set; } 
    }
}
