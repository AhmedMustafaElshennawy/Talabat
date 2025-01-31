using ErrorOr;
using MediatR;
using Talabat.Application.DTOs.product;


namespace Talabat.Application.Features.product.Queries.GetProductById
{
    public record GetProductByIdQuery(int Id):IRequest<ErrorOr<GetProductByIdResponse>>;
}
