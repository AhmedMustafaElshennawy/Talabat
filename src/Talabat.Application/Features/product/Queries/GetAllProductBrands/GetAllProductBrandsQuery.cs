using ErrorOr;
using MediatR;
using Talabat.Application.DTOs.product;

namespace Talabat.Application.Features.product.Queries.GetAllProductBrands
{
    public record GetAllProductBrandsQuery():IRequest<ErrorOr<IEnumerable<GetProductBrandResponse>>>;
}
