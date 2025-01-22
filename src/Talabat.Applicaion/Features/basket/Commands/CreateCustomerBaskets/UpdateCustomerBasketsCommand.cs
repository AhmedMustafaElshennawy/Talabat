﻿using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.basket;

namespace Talabat.Applicaion.Features.basket.Commands.CreateCustomerBaskets
{
    public record UpdateCustomerBasketsCommand(CustomerBasket customerBasket) :IRequest<ErrorOr<CustomerBasket>>;
}