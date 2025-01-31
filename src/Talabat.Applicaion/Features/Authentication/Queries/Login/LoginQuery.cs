using ErrorOr;
using MediatR;
using Talabat.Applicaion.DTOs.Authentication;

namespace Talabat.Application.Features.Authentication.Queries.Login
{
    public record LoginQuery(string Email ,string Password):IRequest<ErrorOr<RegisterDto>>;

}
