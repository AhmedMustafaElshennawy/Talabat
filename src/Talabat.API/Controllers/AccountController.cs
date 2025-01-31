using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.Applicaion.DTOs.Authentication;
using Talabat.Application.DTOs.Authentication;
using Talabat.Application.Features.Authentication.Commands.Logout;
using Talabat.Application.Features.Authentication.Commands.RegisterCommand;
using Talabat.Application.Features.Authentication.Queries.CheckEmailExistence;
using Talabat.Application.Features.Authentication.Queries.GetCurrentUser;
using Talabat.Application.Features.Authentication.Queries.Login;

namespace Talabat.API.Controllers
{
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly ISender _sender;
        public AccountController(ISender sender) => _sender = sender;
        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterRequest request)
        {
            var command = new CreateRegisterCommand(
                request.FName,
                request.LName,
                request.Email,
                request.UserName,
                request.Password,
                request.PhoneNumber);

            var result = await _sender.Send(command);
            var response = result.Match(
                 success =>
                 {
                     RegisterDto registerDto = new RegisterDto
                     {
                         Email = result.Value.User.Email!,
                         FName = result.Value.User.FirstName,
                         LName = result.Value.User.LastName,
                         PhoneNumber = result.Value.User.PhoneNumber!,
                         Token = result.Value.Token,
                         UserName = result.Value.User.UserName!
                     };
                     return Ok(registerDto);
                 },
                 error => Problem(error));

            return response;
        }
        [HttpGet]
        public async Task<IActionResult>Login(LoginRequest request)
        {
            var query = new LoginQuery(
                request.Email, 
                request.Password);

            var result = await _sender.Send(query);
            var response = result.Match(
                success => Ok(result.Value),
                error => Problem(error)
                );

            return response;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            var query = new LogOutQuery();
            var result = await _sender.Send(query);

            var response = result.Match(
                success => Ok(result.Value),
                error => Problem(error)
                );

            return response;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var query = new GetCurrentUserQuery();
            var result = await _sender.Send(query);

            var response = result.Match(
                success => Ok(result.Value),
                error => Problem(error)
                );

            return response;
        }
        [HttpGet]
        public async Task<IActionResult> CheckEmailExists(string Email)
        {
            var query = new CheckEmailExistenceQuery(Email);
            var result = await _sender.Send(query);

            var response = result.Match(
                success => Ok(result.Value),
                error => Problem(error)
                );

            return response;
        }
    }
}