using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Applicaion.DTOs.product;
using Talabat.Domain.productBrand;
using Talabat.Domain.productType;

namespace Talabat.Applicaion.Features.product.Queries.GetAllProductBrands
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
