using ErrorOr;
using MediatR;

namespace Talabat.Application.Features.Authentication.Queries.GetCurrentUser
{
    public record GetCurrentUserQuery():IRequest<ErrorOr<UserInformation>>;

}
