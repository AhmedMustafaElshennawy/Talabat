using ErrorOr;
using MediatR;
using Talabat.Domain.basket;


namespace Talabat.Application.Features.basket.Commands.CreateCustomerBaskets
{
    public record UpdateCustomerBasketsCommand(CustomerBasket customerBasket) :IRequest<ErrorOr<CustomerBasket>>;
}
