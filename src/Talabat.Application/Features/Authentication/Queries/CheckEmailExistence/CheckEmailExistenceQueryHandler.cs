using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Domain.identity;

namespace Talabat.Application.Features.Authentication.Queries.CheckEmailExistence
{
    public class CheckEmailExistenceQueryHandler : IRequestHandler<CheckEmailExistenceQuery, ErrorOr<bool>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CheckEmailExistenceQueryHandler(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ErrorOr<bool>> Handle(CheckEmailExistenceQuery request, CancellationToken cancellationToken)
        {
            //var user = _httpContextAccessor.HttpContext?.User;
            //if (user is null)
            //{
            //    return Error.NotFound(
            //        code: "user.NotFound",
            //        description: "User is not authenticated");
            //}

            //// Extract the email from the claims ==> claim in the token
            //var email = user.FindFirst(ClaimTypes.Email)?.Value;
            //if (string.IsNullOrEmpty(email))
            //{
            //    return Error.NotFound(
            //        code: "email.NotFound",
            //        description: "Email not found in the token");
            //}

            var result = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if (result is null) return false;

            return true;
        }
    }
}
