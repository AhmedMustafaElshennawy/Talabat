using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Infrastructure.Common.Authentication
{
    public class JwtSettings
    {
        public string Audience { get; set; } = null!;
        public const string SectionName = "JWT";
        public double DurationInDays { get; set; }
        public string Issuer { get; set; } = null!;
        public string Key { get; set; } = null!;
    }
}
