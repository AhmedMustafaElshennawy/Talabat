using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.DTOs.customerBasket;
using Talabat.Domain.basket;

namespace Talabat.Applicaion.Common.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<BasketItem, BasketItemDto>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.PictureUrl, src => src.PictureUrl)
                .Map(dest => dest.Brand, src => src.Brand)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.Quantity, src => src.Quantity);

            TypeAdapterConfig<CustomerBasket, CustomerBasketDto>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.PaymentIntentId, src => src.PaymentIntentId)
                .Map(dest => dest.ClientSecret, src => src.ClientSecret)
                .Map(dest => dest.DeliveryMethodId, src => src.DeliveryMethodId)
                .Map(dest => dest.BasketItemDtos, src => src.BasketItems.Adapt<List<BasketItemDto>>());
        }
    }
}
