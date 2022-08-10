using AutoMapper;
using UserMS.Dtos;
using UserMS.Models;

namespace UserMS.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
