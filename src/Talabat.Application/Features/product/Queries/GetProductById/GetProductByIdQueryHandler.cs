using ErrorOr;
using MediatR;
using Talabat.Application.Common.Interfaces.Authentication.Repositories;
using Talabat.Application.DTOs.product;

namespace Talabat.Application.Features.product.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<GetProductByIdResponse>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdQueryHandler(IProductRepository  productRepository) => _productRepository = productRepository;
        public async Task<ErrorOr<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                return Error.NotFound(
                    code:"Product.Notfound",
                    description: "No Product found with this ID.");

            var response = new GetProductByIdResponse
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
                 : new GetProductBrandResponse { Id = 0, ProductBrand = "Unknown" }
                 ,
                ProductType = product.ProductType != null
                 ? new GetProductTypeResponse
                 {
                     Id = product.ProductType.Id,
                     ProducType = product.ProductType.Name
                 }
                 : new GetProductTypeResponse { Id = 0, ProducType = "Unknown" }
            };

            return response;
        }
    }
}