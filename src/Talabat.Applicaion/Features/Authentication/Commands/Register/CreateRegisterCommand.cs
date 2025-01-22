using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.DTOs.Authentication;

namespace Talabat.Applicaion.Features.Authentication.Commands.RegisterCommand
{
    public record CreateRegisterCommand (string FName,
        string LName,   
        string Email,   
        string UserName,
        string Password,  
        string PhoneNumber): IRequest<ErrorOr<RegisterResult>>;
    
}
