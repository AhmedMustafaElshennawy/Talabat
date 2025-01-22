using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Contract.product
{
    public record GetAllProductTypesResponse(IEnumerable<ProducTypeDto> ProducTypes);
    public record ProducTypeDto(int id,string Name);
}
