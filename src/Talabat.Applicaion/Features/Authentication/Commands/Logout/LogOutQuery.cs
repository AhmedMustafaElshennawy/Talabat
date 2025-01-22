using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Applicaion.Features.Authentication.Commands.Logout
{
    public record LogOutQuery():IRequest<ErrorOr<Unit>>;    
}
