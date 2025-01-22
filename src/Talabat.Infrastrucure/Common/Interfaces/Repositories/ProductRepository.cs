using Microsoft.EntityFrameworkCore;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Domain.product;
using Talabat.Infrastructure.Common.Presistance;
using Talabat.Infrastructure.Common.Presistance.specifications;

namespace Talabat.Infrastructure.Common.Interfaces.Repositories
{
    public class ProductRepository : BaseRepository<Product> , IProductRepository
    {
        private readonly TalabatDbContext _talabatDbContext;
        public ProductRepository(TalabatDbContext talabatDbContext):base(talabatDbContext) =>_talabatDbContext = talabatDbContext;
        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var query = await _talabatDbContext.Products
                .Where(X => X.Id == id)
                .Include(X => X.ProductType)
                .Include(X => X.ProductBrand)
                .SingleOrDefaultAsync();
            //var query = await ApplySpecification(new GetProductBiIdSpecifications(id))
            //            .FirstOrDefaultAsync(cancellationToken);
            return query!;
        }
        public IQueryable<Product> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var query = _talabatDbContext.Products
              .Include(x => x.ProductType)
              .Include(x => x.ProductBrand);

            return query;
        }
        private IQueryable<Product> ApplySpecification(Specification<Product> specification)
        {
            return SpecificationEvaluator.GetQuery(
                _talabatDbContext.Set<Product>(), specification);
        }
    }
}
