using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Domain;
using TaskManagement.API.Models.DTO;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly TaskManagementDbContext dbContext;
        private readonly IStatus statusRepositories;
        private readonly IMapper mapper;

        public StatusController(TaskManagementDbContext DbContext, IStatus statusRepositories, IMapper mapper)
        {
            this.dbContext = DbContext;
            this.statusRepositories = statusRepositories;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //Get Data From Database - Repositories Model
            var statuses = await statusRepositories.GetAllAsync();

            //Return DTO's
            return Ok(mapper.Map<List<StatusDTO>>(statuses));
        }
    }
}
