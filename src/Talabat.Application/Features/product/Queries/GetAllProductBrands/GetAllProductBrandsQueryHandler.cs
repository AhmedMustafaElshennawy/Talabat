using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Application.Common.Interfaces.Authentication.Repositories;
using Talabat.Application.DTOs.product;
using Talabat.Domain.productBrand;

namespace Talabat.Application.Features.product.Queries.GetAllProductBrands
{
    public class GetAllProductBrandsQueryHandler : IRequestHandler<GetAllProductBrandsQuery, ErrorOr<IEnumerable<GetProductBrandResponse>>>
    {
        private readonly IBaseRepository<ProductBrand> _repository;
        public GetAllProductBrandsQueryHandler(IBaseRepository<ProductBrand> repository) => _repository = repository;
        public async Task<ErrorOr<IEnumerable<GetProductBrandResponse>>> Handle(GetAllProductBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _repository.GetAllAsync();
            if (brands is null || !brands.Any())
            {
                return Error.NotFound(
                    code: "ProductBrands.NotFound",
                    description: "No ProductBrands found");
            }

            var response = await brands.Select(brand => new GetProductBrandResponse
            { Id = brand.Id, ProductBrand = brand.Name }).ToListAsync(cancellationToken);

            return response;
        }
    }
}
