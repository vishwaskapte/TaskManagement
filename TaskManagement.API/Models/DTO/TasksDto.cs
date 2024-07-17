namespace TaskManagement.API.Models.DTO
{
    public class TasksDto
    {
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public int StatusId { get; set; }
    }
}
