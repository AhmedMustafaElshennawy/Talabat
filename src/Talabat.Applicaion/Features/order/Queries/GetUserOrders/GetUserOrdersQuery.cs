using ErrorOr;
using MediatR;
using Talabat.Domain.order;

namespace Talabat.Application.Features.order.Queries.GetUserOrders
{
    public record GetUserOrdersQuery(string UserId):IRequest<ErrorOr<IReadOnlyList<Order>>>;
    
    
}
