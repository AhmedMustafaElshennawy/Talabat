using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Talabat.Applicaion.Services.order;
using Talabat.Domain.identity;
using Talabat.Domain.order;

namespace Talabat.Applicaion.Features.order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateOrderCommandHandler(IOrderService orderService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
                return Error.NotFound(
                    code: "user.NotFound",
                    description: "No user with these credentials");

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Error.NotFound(
                    code: "user.IdNotFound",
                    description: "No user ID found in claims");

            var currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser == null)
                return Error.NotFound(
                    code: "user.NotFound",
                    description: "User not found");

            var order = await _orderService.CreateOrderAsync(
                currentUser.Email!,
                request.CreateOrderRequest.BasketId,
                request.CreateOrderRequest.DeliveryMethodId,
                request.CreateOrderRequest.ShippingAddress);

            if (order == null)
                return Error.Failure(
                    code: "order.CreationFailed",
                    description: "Failed to create order");

            return order;
        }
    }
}