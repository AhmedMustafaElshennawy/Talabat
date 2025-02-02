using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.baseEntity;

namespace Talabat.Application.Common.Interfaces.Authentication.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Entites();
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<int> CountAsync(IQueryable<TEntity> query, CancellationToken cancellationToken);
        Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id);
        Task<TEntity> AddEntityAsync(TEntity entity);
        //Task<TEntity> UpdateAsync(TEntity entity);
    }
}
