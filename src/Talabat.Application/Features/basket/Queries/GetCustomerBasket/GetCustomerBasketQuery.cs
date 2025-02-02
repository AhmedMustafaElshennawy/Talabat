using ErrorOr;
using MediatR;
using Talabat.Domain.basket;

namespace Talabat.Application.Features.basket.Queries.GetCustomerBasket
{
    public record GetCustomerBasketQuery(string Id):IRequest<ErrorOr<CustomerBasket>>;

}
