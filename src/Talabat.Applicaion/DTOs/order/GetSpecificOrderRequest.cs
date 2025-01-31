using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Application.DTOs.order
{
    public record GetSpecificOrderRequest(string BayerEmail, int OrderId);
    
}
