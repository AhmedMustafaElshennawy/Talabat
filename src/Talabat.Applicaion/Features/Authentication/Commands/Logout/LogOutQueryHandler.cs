using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.identity;

namespace Talabat.Applicaion.Features.Authentication.Commands.Logout
{
    public class LogOutQueryHandler : IRequestHandler<LogOutQuery, ErrorOr<Unit>>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogOutQueryHandler(SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ErrorOr<Unit>> Handle(LogOutQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null)
                return Error.NotFound(
                    code: "user.NotFound",
                    description: "No user with this credintial");

            await _signInManager.SignOutAsync();

            return Unit.Value;
        }
    }
}
