using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Applicaion.Features.Authentication.Queries.CheckEmailExistence
{
    public record CheckEmailExistenceQuery(string Email):IRequest<ErrorOr<bool>>;
   
}
