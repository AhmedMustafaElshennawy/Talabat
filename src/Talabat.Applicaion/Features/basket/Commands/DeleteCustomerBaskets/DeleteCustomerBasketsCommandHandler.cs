using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;

namespace Talabat.Applicaion.Features.basket.Commands.DeleteCustomerBaskets
{
    public class DeleteCustomerBasketsCommandHandler : IRequestHandler<DeleteCustomerBasketsCommand, ErrorOr<Unit>>
    {
        private readonly IBasketRepository _basketRepository;
        public DeleteCustomerBasketsCommandHandler(IBasketRepository basketRepository) => _basketRepository = basketRepository;
        public async Task<ErrorOr<Unit>> Handle(DeleteCustomerBasketsCommand request, CancellationToken cancellationToken)
        {
            var customerBaskets = await _basketRepository.DeleteBasketAsync(request.Id);
            if (customerBaskets is false)
                return Error.NotFound(
                    code: "CustomerBaskets.NotFound",
                    description: "There is no Customer Baskets");


            return Unit.Value;
        }
    }
}
