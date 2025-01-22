using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.baseEntity;

namespace Talabat.Infrastructure.Common.Presistance.specifications
{
    public abstract class Specification<TEntity> where TEntity : BaseEntity
    {
        public Specification(Expression<Func<TEntity, bool>>? criteria) => Criteria = criteria;
        public Expression<Func<TEntity,bool>>? Criteria { get; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new();
        public Expression<Func<TEntity, object>>? OrderByAscExpression { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescExpression { get; private set; }
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
            => Includes.Add(includeExpression);
        protected void AddOrderByAsc(Expression<Func<TEntity, object>> orderByExpression)
            => OrderByAscExpression = orderByExpression;
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByExpression)
          => OrderByDescExpression = orderByExpression;
    }
}