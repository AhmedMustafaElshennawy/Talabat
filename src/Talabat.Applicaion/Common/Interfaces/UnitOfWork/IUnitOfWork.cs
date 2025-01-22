using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Domain.identity;
using Talabat.Domain.order;
using Talabat.Domain.product;
using Talabat.Domain.productBrand;
using Talabat.Domain.productType;

namespace Talabat.Applicaion.Common.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<Product> Products { get; }
        public IBaseRepository<ProductType> ProductTypes { get; }
        public IBaseRepository<ProductBrand> ProductBrands { get; }
        public IBaseRepository<DeliveryMethod> DeliveryMethods { get; }
        public IBaseRepository<Order> Orders { get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}
