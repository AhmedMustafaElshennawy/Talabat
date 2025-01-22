using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Domain.basket;

namespace Talabat.Applicaion.Features.basket.Queries.GetCustomerBasket
{
    public class GetCustomerBasketQueryHandler : IRequestHandler<GetCustomerBasketQuery, ErrorOr<CustomerBasket>>
    {
        private readonly IBasketRepository _basketRepository;
        public GetCustomerBasketQueryHandler(IBasketRepository basketRepository) => _basketRepository = basketRepository;
        public async Task<ErrorOr<CustomerBasket>> Handle(GetCustomerBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketAsync(request.Id);

            if (basket == null)
                new CustomerBasket(request.Id);

            return basket!;
        }
    }
}
