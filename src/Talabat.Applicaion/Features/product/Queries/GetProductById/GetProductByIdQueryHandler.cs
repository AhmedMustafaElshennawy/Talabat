using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Applicaion.DTOs.product;
using Talabat.Domain.product;

namespace Talabat.Applicaion.Features.product.Queries.GetProductById
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