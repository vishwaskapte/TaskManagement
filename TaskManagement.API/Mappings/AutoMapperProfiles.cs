using AutoMapper;
using TaskManagement.API.Models.Domain;
using TaskManagement.API.Models.DTO;

namespace TaskManagement.API.Mappings
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TasksDto, Tasks>().ReverseMap();
            CreateMap<AddTaskRequestDto, Tasks>().ReverseMap();
            CreateMap<UpdateTaskRequestDto, Tasks>().ReverseMap();
            CreateMap<Status, StatusDTO>().ReverseMap();
        }
    }
}
