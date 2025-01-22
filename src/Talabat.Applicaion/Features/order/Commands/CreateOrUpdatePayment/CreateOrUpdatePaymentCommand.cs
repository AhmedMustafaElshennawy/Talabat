using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.DTOs.customerBasket;

namespace Talabat.Applicaion.Features.order.Commands.CreateOrUpdatePayment
{
    public record CreateOrUpdatePaymentCommand(string BasketId):IRequest<ErrorOr<CustomerBasketDto>>;
}
