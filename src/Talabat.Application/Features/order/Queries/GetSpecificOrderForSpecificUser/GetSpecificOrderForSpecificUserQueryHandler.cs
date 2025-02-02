using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Talabat.Application.Common.Interfaces.Authentication.Repositories;
using Talabat.Domain.identity;
using Talabat.Domain.order;

namespace Talabat.Application.Features.order.Queries.GetSpecificOrderForSpecificUser
{
    public class GetSpecificOrderForSpecificUserQueryHandler : IRequestHandler<GetSpecificOrderForSpecificUserQuery, ErrorOr<Order>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBaseRepository<Order> _OrderRepository;
        public GetSpecificOrderForSpecificUserQueryHandler(
                    IHttpContextAccessor httpContextAccessor,
                    UserManager<ApplicationUser> userManager,
                    IBaseRepository<Order> OrderRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _OrderRepository = OrderRepository;
        }
        public async Task<ErrorOr<Order>> Handle(GetSpecificOrderForSpecificUserQuery request, CancellationToken cancellationToken)
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

            var userOrder = await _OrderRepository.GetByIdAsync(request.OrderId);
            if (userOrder == null)
                return Error.NotFound(
                    code: "order.NotFound",
                    description: "No order found with the specified ID");

            if (userOrder.BayerEmail != currentUser.Email)
                return Error.Unauthorized(
                    code: "order.AccessDenied",
                    description: "You do not have permission to access this order");

            return userOrder;
        }
    }
}
