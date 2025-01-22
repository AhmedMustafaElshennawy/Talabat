using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Applicaion.Common.Interfaces.UnitOfWork;
using Talabat.Domain.order;
using Talabat.Domain.product;
using Talabat.Domain.productBrand;
using Talabat.Domain.productType;
using Talabat.Infrastructure.Common.Interfaces.Repositories;

namespace Talabat.Infrastructure.Common.Interfaces.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TalabatDbContext _talabatDbContext;
        public IBaseRepository<Product> Products { get; set; }
        public IBaseRepository<ProductType> ProductTypes { get; set; }
        public IBaseRepository<ProductBrand> ProductBrands { get; set; }
        public IBaseRepository<DeliveryMethod> DeliveryMethods { get; set; }
        public IBaseRepository<Order> Orders { get; set; }
        public UnitOfWork(TalabatDbContext talabatDbContext)
        {
            Products = new BaseRepository<Product>(talabatDbContext);
            ProductTypes = new BaseRepository<ProductType>(talabatDbContext);
            ProductBrands = new BaseRepository<ProductBrand>(talabatDbContext);
            DeliveryMethods = new BaseRepository<DeliveryMethod>(talabatDbContext);
            Orders = new BaseRepository<Order>(talabatDbContext);
            _talabatDbContext = talabatDbContext;
        }
        public int Complete()=>_talabatDbContext.SaveChanges();
        public async Task<int> CompleteAsync() => await _talabatDbContext.SaveChangesAsync();
        public void Dispose() => _talabatDbContext?.Dispose();
    }
}
