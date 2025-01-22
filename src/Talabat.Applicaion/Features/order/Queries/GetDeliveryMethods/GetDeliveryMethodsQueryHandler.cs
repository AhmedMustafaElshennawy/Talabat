using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Domain.identity;
using Talabat.Domain.order;

namespace Talabat.Applicaion.Features.order.Queries.GetDeliveryMethods
{
    public class GetDeliveryMethodsQueryHandler : IRequestHandler<GetDeliveryMethodsQuery, ErrorOr<IReadOnlyList<DeliveryMethod>>>
    {
        private readonly IBaseRepository<DeliveryMethod> _DeliveryMethodRepository;
        public GetDeliveryMethodsQueryHandler(IBaseRepository<DeliveryMethod> deliveryMethodRepository) => _DeliveryMethodRepository = deliveryMethodRepository;
        
        public async Task<ErrorOr<IReadOnlyList<DeliveryMethod>>> Handle(GetDeliveryMethodsQuery request, CancellationToken cancellationToken)
        {
            var deliveryMethods = await _DeliveryMethodRepository.GetAllAsync();
            if (deliveryMethods == null || !deliveryMethods.Any())
            {
                return Error.NotFound(
                    code: "deliveryMethods.NotFound",
                    description: "No delivery methods available.");
            }

            return deliveryMethods.ToList().AsReadOnly();
        }
    }
}
