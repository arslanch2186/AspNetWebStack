using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Db.PagedResults
{
    public static class LinqQueryableExtensions
    {
        public static async Task<PagedResult<T>> GetPagedResult<T>(this IQueryable<T> query,
                                         int page, int pageSize) where T : class
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}
