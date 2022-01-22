using AutoMapper;
using Product.DTOs.ProductDto;

namespace Product.Profiles
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Data.Entities.Product, ProductGetDto>();
            CreateMap<ProductPostDto, Data.Entities.Product>();
        }
    }
}
