using AutoMapper;
using Entities.Models;
using SharedAPI.Data;

namespace StudyBuddy
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserCreationDto, User>();

            CreateMap<Message, HubMessage>();

            CreateMap<Department, DepartmentDto>();

            CreateMap<Course, CourseDto>();

            CreateMap<CourseOfStudy, CourseDto>();
        }
    }
}
