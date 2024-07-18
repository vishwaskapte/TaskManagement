using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Domain;
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

        [HttpPost]
        public IActionResult Create([FromBody]AddTaskRequestDto taskRequestDto) 
        {
            //Map or convert DTO to Domain Model
            var taskDomainModel = new Tasks
            {
                Title = taskRequestDto.Title,
                Description = taskRequestDto.Description,
                DueDate = taskRequestDto.DueDate,
                StatusId = taskRequestDto.StatusId
            };

            //Use Domain Model to Create Tasks
            dbContext.Taskss.Add(taskDomainModel);
            dbContext.SaveChanges();

            //Map Domain Model to DTO

            var TasksDto = new TasksDto()
            {
                TaskId = taskDomainModel.TaskId,
                Title = taskRequestDto.Title,
                Description = taskRequestDto.Description,
                DueDate = taskRequestDto.DueDate,
                StatusId = taskRequestDto.StatusId
            };

            return CreatedAtAction(nameof(GetById), new { id = taskDomainModel.TaskId }, taskDomainModel);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody]UpdateTaskRequestDto updateTaskRequestDto) 
        {
            //check if task exist
            var taskDomainModel = dbContext.Taskss.FirstOrDefault(x => x.TaskId == id);

            if(taskDomainModel == null)
            {
                return NotFound();
            }

            //map DTO to Domain Model
            taskDomainModel.Title = updateTaskRequestDto.Title;
            taskDomainModel.Description = updateTaskRequestDto.Description;
            taskDomainModel.DueDate = updateTaskRequestDto.DueDate;
            taskDomainModel.StatusId = updateTaskRequestDto.StatusId;

            dbContext.SaveChanges();

            //Map Domain Models to DTOs
            var tasksDto = new TasksDto()
            {
                TaskId = taskDomainModel.TaskId,
                Title = taskDomainModel.Title,
                Description = taskDomainModel.Description,
                DueDate = taskDomainModel.DueDate,
                StatusId = taskDomainModel.StatusId
            };

            return Ok(tasksDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute]int id) 
        {
            //check if task exist
            var taskDomainModel = dbContext.Taskss.FirstOrDefault(x => x.TaskId == id);

            if (taskDomainModel == null)
            {
                return NotFound();
            }

            //Delete Tasks
            dbContext.Taskss.Remove(taskDomainModel);
            dbContext.SaveChanges();

            //Map Domain Models to DTOs
            var tasksDto = new TasksDto()
            {
                TaskId = taskDomainModel.TaskId,
                Title = taskDomainModel.Title,
                Description = taskDomainModel.Description,
                DueDate = taskDomainModel.DueDate,
                StatusId = taskDomainModel.StatusId
            };

            return Ok(tasksDto);
        }
    }
}
