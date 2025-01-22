using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Contract.product
{
    public record GetAllProductsResponse(
        IEnumerable<ProductResponse> Products,
        int CurrentPage,
        int PageSize,
        int TotalPages,
        int TotalCount
        );

    public record ProductResponse(
       int Id,
       string Name,
       string Description,
       string PictureUrl,
       decimal Price,
       ProducTypeResponse Brand,
       ProducBrandResponse Type
        );
   
    public record ProducTypeResponse(int Id,string Name);
    public record ProducBrandResponse(int Id, string Name);
}