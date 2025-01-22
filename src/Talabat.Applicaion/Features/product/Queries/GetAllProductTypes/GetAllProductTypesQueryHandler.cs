using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Applicaion.DTOs.product;
using Talabat.Applicaion.Features.product.Queries.GetAllProductTypes;
using Talabat.Domain.productType;

namespace Talabat.Applicaion.Features.product.Queries.GetAllProductBrands
{
    public class GetAllProductTypesQueryHandler : IRequestHandler<GetAllProductTypesQuery, ErrorOr<List<GetProductTypeResponse>>>
    {
        private readonly IBaseRepository<ProductType> _repository;
        public GetAllProductTypesQueryHandler(IBaseRepository<ProductType> repository) => _repository = repository;
        public async Task<ErrorOr<List<GetProductTypeResponse>>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _repository.GetAllAsync();

            if (types is null || !types.Any())
            {
                return Error.NotFound(
                    code: "ProductType.NotFound",
                    description: "No product types are found");
            }

            var response = types.Select(type => new GetProductTypeResponse
            {
                Id = type.Id, 
                ProducType = type.Name 
            }).ToList(); 

            return response;
        }
    }
}
