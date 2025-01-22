using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.DTOs.product;
using Talabat.Domain.product;

namespace Talabat.Applicaion.Features.product.Queries.GetProductById
{
    public record GetProductByIdQuery(int Id):IRequest<ErrorOr<GetProductByIdResponse>>;
}
