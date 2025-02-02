using ErrorOr;
using MediatR;
using Talabat.Application.DTOs.customerBasket;


namespace Talabat.Application.Features.order.Commands.CreateOrUpdatePayment
{
    public record CreateOrUpdatePaymentCommand(string BasketId):IRequest<ErrorOr<CustomerBasketDto>>;
}
