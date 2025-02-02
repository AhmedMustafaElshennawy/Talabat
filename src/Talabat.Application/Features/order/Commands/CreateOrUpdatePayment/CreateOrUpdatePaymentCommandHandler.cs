using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using Talabat.Applicaion.Services.Payment;
using Talabat.Application.DTOs.customerBasket;

namespace Talabat.Application.Features.order.Commands.CreateOrUpdatePayment
{
    public class CreateOrUpdatePaymentCommandHandler : IRequestHandler<CreateOrUpdatePaymentCommand, ErrorOr<CustomerBasketDto>>
    {
        private readonly IPaymentService _paymentService;
        public CreateOrUpdatePaymentCommandHandler(IPaymentService paymentService, IMapper mapper) => _paymentService = paymentService;
        public async Task<ErrorOr<CustomerBasketDto>> Handle(CreateOrUpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(request.BasketId);
            if (basket == null)
            {
                return Error.NotFound(
                    code: "basket.NotFound",
                    description: "No basket found with the provided ID or error occurred during payment processing.");
            }

            var response = basket.Adapt<CustomerBasketDto>();
            return response;
        }
    }
}
