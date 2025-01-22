using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.baseEntity;
using Talabat.Domain.product;

namespace Talabat.Domain.productType
{
    public class ProductType:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> products { get; set; }= new HashSet<Product>();
    }
}
