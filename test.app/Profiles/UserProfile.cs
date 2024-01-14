using AutoMapper;
using test.app.DTOs;
using test.domain.Entities;

namespace test.app.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap()
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName));
    }
}
