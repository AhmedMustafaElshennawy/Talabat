using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Domain.baseEntity;
using Talabat.Infrastructure.Common.Presistance;

namespace Talabat.Infrastructure.Common.Interfaces.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly TalabatDbContext _talabatDbContext;
        public BaseRepository(TalabatDbContext talabatDbContext) => _talabatDbContext = talabatDbContext;
        public IQueryable<TEntity> Entites() => _talabatDbContext.Set<TEntity>();
        public async Task<IQueryable<TEntity>> GetAllAsync() => _talabatDbContext.Set<TEntity>().AsNoTracking();
        public async Task<TEntity> GetByIdAsync(int id) => await _talabatDbContext.Set<TEntity>().FirstAsync(x => x.Id == id);
        public async Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query, CancellationToken cancellationToken)
            => await query.ToListAsync(cancellationToken);
        public async Task<int> CountAsync(IQueryable<TEntity> query, CancellationToken cancellationToken)
            => await query.CountAsync(cancellationToken);
        public async Task<bool> DeleteAsync(int id)
        {
            var Entity = await _talabatDbContext.Set<TEntity>().FirstAsync(x => x.Id == id);
            var Result = _talabatDbContext.Set<TEntity>().Remove(Entity);
            if (Result is not null) return true;

            return false;  
        }
        public async Task<TEntity> AddEntityAsync(TEntity entity)
        {
            var result = await _talabatDbContext.AddAsync(entity);
            return entity;
        }
    }
}