using ErrorOr;
using MediatR;
using Talabat.Application.DTOs.order;
using Talabat.Domain.order;

namespace Talabat.Application.Features.order.Commands.CreateOrder
{
    public record CreateOrderCommand(CreateOrderRequest CreateOrderRequest) : IRequest<ErrorOr<Order>>;

}
