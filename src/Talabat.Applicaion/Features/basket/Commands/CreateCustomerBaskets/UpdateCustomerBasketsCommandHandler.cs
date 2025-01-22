using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Domain.basket;

namespace Talabat.Applicaion.Features.basket.Commands.CreateCustomerBaskets
{
    public class UpdateCustomerBasketsCommandHandler : IRequestHandler<UpdateCustomerBasketsCommand, ErrorOr<CustomerBasket>>
    {
        private readonly IBasketRepository _basketRepository;
        public UpdateCustomerBasketsCommandHandler(IBasketRepository basketRepository) => _basketRepository = basketRepository;
        public async Task<ErrorOr<CustomerBasket>> Handle(UpdateCustomerBasketsCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.UpdateBasketAsync(request.customerBasket);
            if (basket is null)
                return Error.NotFound(
                    code: "CustomerBaskets.NotFound",
                    description: "There is no Customer Baskets");

            return basket;
        }
    }
}
