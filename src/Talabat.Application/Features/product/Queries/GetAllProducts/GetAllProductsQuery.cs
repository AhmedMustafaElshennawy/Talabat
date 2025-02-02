using ErrorOr;
using MediatR;
using Talabat.Application.DTOs.product;
using Talabat.Shared.Paging;
using Talabat.Shared.Paging.Paging;

namespace Talabat.Application.Features.product.Queries.GetAllProducts
{
    public record GetAllProductsQuery():PaginatedRequest,IRequest<ErrorOr<PaginatedResponse<GetAllProductResponse>>>;
}
