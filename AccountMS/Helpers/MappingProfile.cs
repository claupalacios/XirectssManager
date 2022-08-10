using AccountMS.Dtos;
using AccountMS.Models;
using AutoMapper;

namespace AccountMS.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}
