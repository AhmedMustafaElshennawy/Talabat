using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.baseEntity;
using Talabat.Domain.product;

namespace Talabat.Application.Common.Interfaces.Authentication.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);
        public IQueryable<Product> GetAllProductsAsync(CancellationToken cancellationToken);
    }
}
