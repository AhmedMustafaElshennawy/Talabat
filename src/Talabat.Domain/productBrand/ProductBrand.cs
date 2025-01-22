using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.baseEntity;
using Talabat.Domain.product;

namespace Talabat.Domain.productBrand
{
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get;set; } = new HashSet<Product>();
    }
}
