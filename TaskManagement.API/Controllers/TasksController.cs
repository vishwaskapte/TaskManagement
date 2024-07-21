using AutoMapper;
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
        private readonly IMapper mapper;

        public TasksController(TaskManagementDbContext DbContext, ITasks tasksRepositories, IMapper mapper)
        {
            this.dbContext = DbContext;
            this.tasksRepositories = tasksRepositories;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Repositories Model
            var tasks = await tasksRepositories.GetAllAsync();

            //Return DTO's
             return Ok(mapper.Map<List<TasksDto>>(tasks));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            //Get Data From Database - Domain Model
            var tasks = await tasksRepositories.GetByIdAsync(id);

            if (tasks == null) { return NotFound(); }

            //Return DTO's
            return Ok(mapper.Map<TasksDto>(tasks));

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddTaskRequestDto taskRequestDto) 
        {
            //Map or convert DTO to Domain Model
            var taskDomainModel = mapper.Map<Tasks>(taskRequestDto);

            //Use Domain Model to Create Tasks
            taskDomainModel = await tasksRepositories.CreateAsync(taskDomainModel);

            //Map Domain Model to DTO
            var taskDto = mapper.Map<TasksDto>(taskDomainModel);
          
            return CreatedAtAction(nameof(GetById), new { id = taskDomainModel.TaskId }, taskDomainModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]UpdateTaskRequestDto updateTaskRequestDto) 
        {
            //Map DTO to Domain Model
            var TasksDomainModel = mapper.Map<Tasks>(updateTaskRequestDto);

            //check if task exist
            TasksDomainModel = await tasksRepositories.UpdateAsync(id, TasksDomainModel);

            if(TasksDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Models to DTOs
            var taskDto = mapper.Map<TasksDto>(TasksDomainModel);

            return Ok(taskDto);
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
            taskDomainModel = await tasksRepositories.DeleteAsync(id);

            //Map Domain Models to DTOs
            var tasksDto = mapper.Map<TasksDto>(taskDomainModel);

            return Ok(tasksDto);
        }
    }
}
