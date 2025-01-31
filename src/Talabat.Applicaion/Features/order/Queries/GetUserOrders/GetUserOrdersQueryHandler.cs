using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Talabat.Application.Common.Interfaces.Authentication.Repositories;
using Talabat.Application.Features.order.Queries.GetUserOrders;
using Talabat.Domain.identity;
using Talabat.Domain.order;

namespace Talabat.Applicaion.Features.order.Queries.GetUserOrders
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, ErrorOr<IReadOnlyList<Order>>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBaseRepository<Order> _OrderRepository;
        public GetUserOrdersQueryHandler(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IBaseRepository<Order> OrderRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _OrderRepository = OrderRepository;
        }
        public async Task<ErrorOr<IReadOnlyList<Order>>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
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

            var orders = await _OrderRepository.GetAllAsync();
            var userOrders = orders.Where(o => o.BayerEmail == currentUser.Email).ToList();

            if (orders == null || !orders.Any())
                return Error.NotFound(
                    code: "order.NotFound",
                    description: "No orders found for the user");

            return userOrders.AsReadOnly();
        }
    }
}