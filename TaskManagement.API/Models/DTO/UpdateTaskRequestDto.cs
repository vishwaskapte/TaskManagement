using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.Models.DTO
{
    public class UpdateTaskRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Title has to be a maximum of 50 characters")]
        public string? Title { get; set; }

        [MaxLength(200, ErrorMessage = "Description has to be a maximum of 200 characters")]
        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int StatusId { get; set; }
    }
}
