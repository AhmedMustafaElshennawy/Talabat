using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.order;

namespace Talabat.Applicaion.Features.order.Queries.GetSpecificOrderForSpecificUser
{
    public record GetSpecificOrderForSpecificUserQuery
        (string BayerEmail, 
        int OrderId):IRequest<ErrorOr<Order>>;

    
}
