using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.identity;

namespace Talabat.Applicaion.Features.Authentication.Queries.GetCurrentUser
{
    public record UserInformation(
        string Email , 
        string UserName,
        string FirstName,
        string LastName);

    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ErrorOr<UserInformation>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetCurrentUserQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<ErrorOr<UserInformation>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user is null)
                return Error.NotFound(
                    code: "user.NotFound",
                    description: "No user with this credintial");

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Error.NotFound(
                    code: "user.IdNotFound",
                    description: "No user ID found in claims");

            var applicationUser = await _userManager.FindByIdAsync(userId);
            if (applicationUser == null)
                return Error.NotFound(
                    code: "user.NotFound",
                    description: "No user found with this ID");

            var response = new UserInformation(
                applicationUser.Email!,
                applicationUser.UserName!,
                applicationUser.FirstName!,
                applicationUser.LastName!);

            return response;
        }
    }
}
