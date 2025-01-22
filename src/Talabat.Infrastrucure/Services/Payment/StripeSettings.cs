using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Infrastructure.Services.Payment
{
    public class StripeSettings
    {
        public const string SectionName = "Stripe";
        public string Publishablekey { get; set; } = null!;
        public string Secretkey { get; set; } = null!;
    }
}
