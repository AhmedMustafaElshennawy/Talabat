using ErrorOr;
using MediatR;
using Talabat.Application.DTOs.product;

namespace Talabat.Application.Features.product.Queries.GetAllProductTypes
{
    public record GetAllProductTypesQuery():IRequest<ErrorOr<List<GetProductTypeResponse>>>;
}
