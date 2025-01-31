using ErrorOr;
using MediatR;

namespace Talabat.Application.Features.Authentication.Queries.CheckEmailExistence
{
    public record CheckEmailExistenceQuery(string Email):IRequest<ErrorOr<bool>>;
   
}
