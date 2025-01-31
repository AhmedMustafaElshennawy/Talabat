using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.identity;

namespace Talabat.Application.Common.Interfaces.Authentication
{
    public interface ITokenGenerator
    {
        public Task<string> GenerateToken(ApplicationUser user, List<string> Roles);
        public Task<string> GenerateToken(ApplicationUser user, string Role);
    }
}
