using ErrorOr;
using MediatR;
using Talabat.Domain.order;

namespace Talabat.Application.Features.order.Queries.GetDeliveryMethods
{
    public record GetDeliveryMethodsQuery():IRequest<ErrorOr<IReadOnlyList<DeliveryMethod>>>;
   
}
