using AutoMapper;
using test.app.DTOs;
using test.domain.Entities;

namespace test.app.Profiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>().ReverseMap();
    }

}