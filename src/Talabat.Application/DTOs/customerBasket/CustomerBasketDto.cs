using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Application.DTOs.customerBasket
{
    public record CustomerBasketDto(
        string Id, 
        string? PaymentIntentId,
        string? ClientSecret,
        int? DeliveryMethodId,
        List<BasketItemDto> BasketItemDtos);


    public record BasketItemDto(
        int Id,
        string Name,
        string PictureUrl,
        string Brand,
        string Type,
        decimal Price,
        int Quantity);
}
