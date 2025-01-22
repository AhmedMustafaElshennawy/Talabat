using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.product;

namespace Talabat.Infrastructure.Common.Presistance.specifications
{
    public class GetProductBiIdSpecifications:Specification<Product>
    {
        public GetProductBiIdSpecifications(int productId) 
            : base(product => product.Id == productId)
        {
            AddInclude(product=>product.ProductType);
            AddInclude(product=>product.ProductBrand);
        }
    }
}
