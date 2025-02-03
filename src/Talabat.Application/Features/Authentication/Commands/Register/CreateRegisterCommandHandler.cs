using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Talabat.Applicaion.Common.Interfaces.UnitOfWork;
using Talabat.Application.Common.Interfaces.Authentication;
using Talabat.Domain.identity;
using Error = ErrorOr.Error;

namespace Talabat.Application.Features.Authentication.Commands.RegisterCommand
{
    public record RegisterResult(
        ApplicationUser User,
        string Token);

    public class CreateRegisterCommandHandler : IRequestHandler<CreateRegisterCommand, ErrorOr<RegisterResult>>
    {
        private readonly string _defaultRole = "shopper";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        public CreateRegisterCommandHandler(ITokenGenerator tokenGenerator,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }
        public async Task<ErrorOr<RegisterResult>> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
        {
            var roleExists = await _roleManager.RoleExistsAsync(_defaultRole);
            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(_defaultRole));
                if (!roleResult.Succeeded)
                {
                    var errorMessages = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    return Error.Failure($"Failed to create role: {errorMessages}");
                }
            }

            if (await _userManager.FindByEmailAsync(request.Email) is not null)
            {
                return Error.Failure(
                    code: "Email.Failure",
                    description: "This email is already registerd , pleas use another one.");
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = request.FName,
                LastName = request.LName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                return Error.Failure(
                    code: "result.Failure",
                    description: $"User creation failed: {errorMessages}");
            }
            
            var addToRoleResult = await _userManager.AddToRoleAsync(user, _defaultRole);
            if (!addToRoleResult.Succeeded)
            {
                var errorMessages = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                return Error.Failure($"Failed to add user to role: {errorMessages}");
            }

            await _unitOfWork.CompleteAsync();
            string token = await _tokenGenerator.GenerateToken(user, _defaultRole);

            var response = new RegisterResult(user, token);
            return response;
        }
    }
}