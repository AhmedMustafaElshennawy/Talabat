using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.order;

namespace Talabat.Domain.identity
{
    public class ApplicationUser:IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public ICollection<Order> Orders { get; set; }= new HashSet<Order>();
    }
}
