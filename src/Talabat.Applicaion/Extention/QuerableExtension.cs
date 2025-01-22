using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.baseEntity;
using Talabat.Shared.Paging;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Talabat.Applicaion.Extention
{
    public static class QuerableExtension
    {
        public static async Task<PaginatedResponse<TEntity>> ToPaginatedListAsync<TEntity>(
            this IQueryable<TEntity> source,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken) where TEntity : BaseEntity
        {
            int count = await source.CountAsync();
            pageSize = pageSize == 0 ? 10 : pageSize;

            List<TEntity> items = pageNumber > 1 ? await source
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize).ToListAsync(cancellationToken)
                                : await source.Take(pageSize).ToListAsync(cancellationToken);

            return PaginatedResponse<TEntity>.Create(items, count, pageNumber, pageSize);
        }
    }
}
