using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Applicaion.Features.basket.Commands.DeleteCustomerBaskets
{
    public record DeleteCustomerBasketsCommand(string Id) : IRequest<ErrorOr<Unit>>;

}
