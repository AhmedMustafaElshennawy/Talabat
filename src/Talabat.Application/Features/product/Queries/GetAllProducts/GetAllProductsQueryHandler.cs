using ErrorOr;
using MediatR;
using System.Linq.Expressions;
using Talabat.Application.Common.Interfaces.Authentication.Repositories;
using Talabat.Application.DTOs.product;
using Talabat.Application.Extensions;
using Talabat.Domain.product;
using Talabat.Shared.Paging;

namespace Talabat.Application.Features.product.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ErrorOr<PaginatedResponse<GetAllProductResponse>>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository) => _productRepository = productRepository;
        public async Task<ErrorOr<PaginatedResponse<GetAllProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> query = _productRepository.GetAllProductsAsync(cancellationToken);
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                var parameter = Expression.Parameter(typeof(Product), "p");
                var property = Expression.Property(parameter, request.SortBy);
                var lambda = Expression.Lambda<Func<Product, object>>(Expression.Convert(property, typeof(object)), parameter);

                if (request.SortOrder.ToLower() == "desc")
                {
                    query = query.OrderByDescending(lambda);
                }
                else
                {
                    query = query.OrderBy(lambda);
                }
            }

            var paginatedProducts = await query.ToPaginatedListAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            if (paginatedProducts is null || !paginatedProducts.Data.Any())
            {
                return Error.NotFound(
                   code: "Products.NotFound",
                   description: "There Is No Products Found.");
            }

            var products = paginatedProducts.Data.Select(product => new GetAllProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand != null
                 ? new GetProductBrandResponse
                 {
                     Id = product.ProductBrand.Id,
                     ProductBrand = product.ProductBrand.Name
                 }
                 : new GetProductBrandResponse { Id = 0, ProductBrand = "Unknown" },
                ProductType = product.ProductType != null
                ? new GetProductTypeResponse
                {
                    Id = product.ProductType.Id,
                    ProducType = product.ProductType.Name
                }
                : new GetProductTypeResponse { Id = 0, ProducType = "Unknown" }
            }).ToList();

            var response = PaginatedResponse<GetAllProductResponse>.Create(
                products,
                paginatedProducts.TotalCount,
                paginatedProducts.CurrentPage,
                paginatedProducts.PageSize);

            return response;
        }
    }
}