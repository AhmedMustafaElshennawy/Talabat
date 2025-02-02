using ErrorOr;
using MediatR;
using Talabat.Application.Common.Interfaces.Authentication.Repositories;
using Talabat.Domain.order;

namespace Talabat.Application.Features.order.Queries.GetDeliveryMethods
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
