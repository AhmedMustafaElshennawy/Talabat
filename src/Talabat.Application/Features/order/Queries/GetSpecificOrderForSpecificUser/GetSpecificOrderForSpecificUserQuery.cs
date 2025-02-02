using ErrorOr;
using MediatR;
using Talabat.Domain.order;

namespace Talabat.Application.Features.order.Queries.GetSpecificOrderForSpecificUser
{
    public record GetSpecificOrderForSpecificUserQuery
        (string BayerEmail, 
        int OrderId):IRequest<ErrorOr<Order>>;

    
}
