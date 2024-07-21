using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Domain;
using TaskManagement.API.Models.DTO;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskManagementDbContext dbContext;
        private readonly ITasks tasksRepositories;

        public TasksController(TaskManagementDbContext DbContext, ITasks tasksRepositories)
        {
            this.dbContext = DbContext;
            this.tasksRepositories = tasksRepositories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Repositories Model
            var tasks = await tasksRepositories.GetAllAsync();

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
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            //Get Data From Database - Domain Model
            var tasks = await tasksRepositories.GetByIdAsync(id);

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
        public async Task<IActionResult> Create([FromBody]AddTaskRequestDto taskRequestDto) 
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
            await dbContext.Taskss.AddAsync(taskDomainModel);
            await dbContext.SaveChangesAsync();

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
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]UpdateTaskRequestDto updateTaskRequestDto) 
        {
            //Map DTO to Domain Model
            var TasksDomainModel = new Tasks
            {
                Title = updateTaskRequestDto.Title,
                Description = updateTaskRequestDto.Description,
                DueDate = updateTaskRequestDto.DueDate,
                StatusId = updateTaskRequestDto.StatusId
            };

            //check if task exist
            TasksDomainModel = await tasksRepositories.UpdateAsync(id, TasksDomainModel);

            if(TasksDomainModel == null)
            {
                return NotFound();
            }

            await dbContext.SaveChangesAsync();

            //Map Domain Models to DTOs
            var tasksDto = new TasksDto()
            {
                TaskId = TasksDomainModel.TaskId,
                Title = TasksDomainModel.Title,
                Description = TasksDomainModel.Description,
                DueDate = TasksDomainModel.DueDate,
                StatusId = TasksDomainModel.StatusId
            };

            return Ok(tasksDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id) 
        {
            //check if task exist
            var taskDomainModel = await tasksRepositories.DeleteAsync(id);

            if (taskDomainModel == null)
            {
                return NotFound();
            }

            //Delete Tasks
            dbContext.Taskss.Remove(taskDomainModel);
            await dbContext.SaveChangesAsync();

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
