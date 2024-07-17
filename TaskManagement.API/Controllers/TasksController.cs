using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.API.Data;
using TaskManagement.API.Models.DTO;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskManagementDbContext dbContext;

        public TasksController(TaskManagementDbContext DbContext)
        {
            dbContext = DbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data From Database - Domain Model
            var tasks = dbContext.Taskss.ToList();

            //Map Domain Models to DTOs
            var tasksDto = new List<TasksDto>();
            foreach (var task in tasks) {
                tasksDto.Add(new TasksDto()
                {
                    TaskId = task.TaskId,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    StatusId = task.StatusId
                });
            }
            return Ok(tasksDto);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            //Get Data From Database - Domain Model
            var tasks = dbContext.Taskss.FirstOrDefault(x => x.TaskId == id);

            if (tasks == null) { return NotFound(); }

            //Map Domain Models to DTOs
            var tasksDto = new TasksDto()
            {
                TaskId = tasks.TaskId,
                Title = tasks.Title,
                Description = tasks.Description,
                DueDate = tasks.DueDate,
                StatusId = tasks.StatusId
            };

            return Ok(tasksDto);

        }
    }
}
