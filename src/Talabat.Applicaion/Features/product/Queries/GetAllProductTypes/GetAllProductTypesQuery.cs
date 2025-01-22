using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.DTOs.product;
using Talabat.Domain.productBrand;
using Talabat.Domain.productType;

namespace Talabat.Applicaion.Features.product.Queries.GetAllProductTypes
{
    public record GetAllProductTypesQuery():IRequest<ErrorOr<List<GetProductTypeResponse>>>;
}
