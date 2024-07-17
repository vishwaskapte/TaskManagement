using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.Models.Domain
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }

        public int StatusId { get; set; }

        //navigation property
        public Status? Status { get; set; }

    }
}
