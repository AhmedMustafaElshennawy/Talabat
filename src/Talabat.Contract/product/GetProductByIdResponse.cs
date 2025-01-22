using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Contract.product
{
    public record GetProductByIdResponse(
        int Id,
        string Name,
        string Description,
        string PictureUrl,
        decimal Price,
        producBrandResponse ProductBrand,
        producTypeResponse ProductType,
        int ProductBrandId,
        int ProductTypeId);
    public record producTypeResponse(int Id, string Name);
    public record producBrandResponse(int Id, string Name);
}
