using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.baseEntity;

namespace Talabat.Domain.identity
{
    public class Address:BaseEntity
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
