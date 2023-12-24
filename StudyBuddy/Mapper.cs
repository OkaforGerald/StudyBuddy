using AutoMapper;
using Entities.Models;
using Shared.Data_Transfer;

namespace StudyBuddy
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserCreationDto, User>();
        }
    }
}
