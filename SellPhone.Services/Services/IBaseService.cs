using SellPhone.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.Services
{
    public interface IBaseService<T, TKey> where T : SoftDeletedEntity<TKey>, new()
    {
        Task<T> Get(TKey id);
        Task<T> Find(Expression<Func<T, bool>> filters);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filters);
        Task Add(T obj);
        Task AddRange(IEnumerable<T> objects);
        Task Update(T obj);
        Task UpdateRange(IEnumerable<T> objects);
        Task Delete(TKey id);
        Task DeleteRange(IEnumerable<T> objects);
    }
}
