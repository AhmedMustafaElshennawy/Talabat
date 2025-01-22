using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.DTOs.order;
using Talabat.Domain.order;

namespace Talabat.Applicaion.Features.order.Commands.CreateOrder
{
    public record CreateOrderCommand(CreateOrderRequest CreateOrderRequest) : IRequest<ErrorOr<Order>>;

}
