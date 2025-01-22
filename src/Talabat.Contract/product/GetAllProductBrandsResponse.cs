using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Contract.product
{
    public record GetAllProductBrandsResponse(IEnumerable<ProducBrandDto> ProducBrand);
    public record ProducBrandDto(int id,string Name);
}
