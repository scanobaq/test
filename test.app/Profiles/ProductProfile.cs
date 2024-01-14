using AutoMapper;
using test.app.DTOs;
using test.domain.Entities;

namespace test.app.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }

}
