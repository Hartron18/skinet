using AutoMapper;
using SkinetApi.Dtos;
using SkinetApi.Entities;

namespace SkinetApi.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(x => x.ProductBrand, o => o.MapFrom(x => x.ProductBrand.Name))
                .ForMember(x => x.ProductType, o => o.MapFrom(x => x.ProductType.Name))
                .ForMember(x => x.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }        
    }
}
