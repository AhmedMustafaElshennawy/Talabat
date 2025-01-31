using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Talabat.Applicaion.DTOs.Authentication;
using Talabat.Application.Common.Interfaces.Authentication;
using Talabat.Domain.identity;

namespace Talabat.Application.Features.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<RegisterDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenGenerator _tokenService; 
        //private readonly RoleManager<> _tokenService; 

        public LoginQueryHandler(UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  ITokenGenerator tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<ErrorOr<RegisterDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Error.NotFound(
                    code: "user.NotFound",
                    description: "There is no user with this Email.");

            //var result = await _userManager.CheckPasswordAsync(user, request.Password);
            //if (!result)
            //    return Error.Failure(
            //            code: "user.Failure",
            //            description: "The password is wrong.");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!result.Succeeded)
                return Error.Failure(
                        code: "user.Failure",
                        description: "The password is wrong.");

            var roles = await _userManager.GetRolesAsync(user);
            var rolesList = roles.ToList();

            var token = await _tokenService.GenerateToken(user, rolesList);
            var response = new RegisterDto
            {
                FName = user.FirstName,  
                LName = user.LastName,   
                Email = user.Email!,
                UserName = user.UserName!,
                PhoneNumber = user.PhoneNumber!,
                Token = token 
            };

            return response;
        }
    }
}
