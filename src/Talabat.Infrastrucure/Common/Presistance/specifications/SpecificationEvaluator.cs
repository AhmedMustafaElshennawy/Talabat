using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.baseEntity;

namespace Talabat.Infrastructure.Common.Presistance.specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(
            IQueryable<TEntity> entities,
            Specification<TEntity> specification) 
            where TEntity : BaseEntity
        {
            IQueryable<TEntity> query = entities;
            if (specification.Criteria is not null)
            {
                 query = query.Where(specification.Criteria);   
            }

            specification.Includes.Aggregate(
                query,
                (current, includeExpression)
                    => current.Include(includeExpression));

            if (specification.OrderByAscExpression is not null)
            {
                query = query.OrderBy(specification.OrderByAscExpression);  
            }
            if (specification.OrderByDescExpression is not null)
            {
                query = query.OrderBy(specification.OrderByDescExpression);
            }

            return query;
        }
    }
}
