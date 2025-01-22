using Mapster;
using Talabat.Domain.product;
using Talabat.Domain.productBrand;
using Talabat.Domain.productType;
using Talabat.Shared.Paging;

namespace Talabat.API.MappingConfiguration
{
    public static class MappingConfigurations
    {
        public static void Register(TypeAdapterConfig config)
        {
            //Mapping Product to ProductDto
            //config.NewConfig<Product, ProductResponse>()
            //    .Map(dest => dest.Name, src => src.Name)
            //    .Map(dest => dest.Description, src => src.Description)
            //    .Map(dest => dest.PictureUrl, src => src.PictureUrl);


            //// Map Product to ProductResponse
            //TypeAdapterConfig<Product, ProductResponse>.NewConfig()
            //    .Map(dest => dest.Id, src => src.Id)
            //    .Map(dest => dest.Name, src => src.Name)
            //    .Map(dest => dest.Description, src => src.Description)
            //    .Map(dest => dest.PictureUrl, src => src.PictureUrl)
            //    .Map(dest => dest.Price, src => src.Price)
            //    .Map(dest => dest.Brand, src => src.ProductBrand)  
            //    .Map(dest => dest.Type, src => src.ProductType);  

        }
    }
}