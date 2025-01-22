using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Talabat.Applicaion.Features.product.Queries.GetAllProducts;
using Talabat.Applicaion.Features.product.Queries.GetProductById;
using Talabat.Applicaion.Features.product.Queries.GetAllProductTypes;
using Talabat.Shared.Paging;
using Talabat.Applicaion.DTOs.product;
using Talabat.Applicaion.Features.product.Queries.GetAllProductBrands;

namespace Talabat.API.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ISender _sender;
        public ProductController(ISender sender) => _sender = sender;
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<GetAllProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = "price",
            [FromQuery] string? sortOrder = "asc")
        {
            var query = new GetAllProductsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SortOrder = sortOrder!
            };

            var result = await _sender.Send(query);
            var response = result.Match(
                success => Ok(result.Value), 
                error => Problem(error)
                );

            return response;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetProductByIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById([FromQuery] int Id)
        {
            var query = new GetProductByIdQuery(Id);
            var result = await _sender.Send(query);

            var response = result.Match(
                success => Ok(result.Value),
                error => Problem(error)
                );

            return response;
        }

        [HttpGet("Types")]
        [ProducesResponseType(typeof(GetProductTypeResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTypes()
        {
            var query = new GetAllProductTypesQuery();
            var result = await _sender.Send(query);

            var response = result.Match(
                success => Ok(result.Value),
                error => Problem(error)
                );

            return response;
        }

        [HttpGet("Brands")]
        [ProducesResponseType(typeof(GetProductBrandResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrands()
        {
            var query = new GetAllProductBrandsQuery();
            var result = await _sender.Send(query);
             
            var response = result.Match(
                success => Ok(result.Value),
                error => Problem(error)
                );
            
            return response;
        }
    }
}