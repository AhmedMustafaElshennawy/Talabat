using ErrorOr;
using MediatR;

namespace Talabat.Application.Features.basket.Commands.DeleteCustomerBaskets
{
    public record DeleteCustomerBasketsCommand(string Id) : IRequest<ErrorOr<Unit>>;

}
