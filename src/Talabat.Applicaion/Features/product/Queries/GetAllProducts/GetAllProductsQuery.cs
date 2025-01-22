using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.DTOs.product;
using Talabat.Domain.product;
using Talabat.Shared.Paging;
using Talabat.Shared.Paging.Paging;

namespace Talabat.Applicaion.Features.product.Queries.GetAllProducts
{
    public record GetAllProductsQuery():PaginatedRequest,IRequest<ErrorOr<PaginatedResponse<GetAllProductResponse>>>;
}
