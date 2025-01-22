using Azure.Messaging;
using ErrorOr;
using FluentValidation;
using Talabat.Domain.product;

namespace Talabat.Applicaion.Features.product.Queries.GetAllProducts
{
    public class GetAllProductsQueryValidator:AbstractValidator<GetAllProductsQuery>
    {
        public GetAllProductsQueryValidator()
        {
            RuleFor(x => x.SortBy)
                .Must(BeAValidSortProperty!)
                .WithMessage("Invalid SortBy property.");

            RuleFor(x => x.SortOrder)
                .Must(order => order.ToLower() == "asc" || order.ToLower() == "desc")
                .WithMessage("SortOrder must be 'asc' or 'desc'.");
        }
        private bool BeAValidSortProperty(string sortBy)
        {
            var validProperties = typeof(Product).GetProperties()
                .Select(property => property.Name.ToLower())
                .ToList();

            var result = validProperties.Contains(sortBy.ToLower());
            return result;
        }
    }
}
