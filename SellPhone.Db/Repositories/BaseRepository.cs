using Microsoft.EntityFrameworkCore;
using SellPhone.Db.Data;
using SellPhone.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Db.Repositories
{
    public class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : SoftDeletedEntity<TKey>, new()
    {
        protected readonly SellPhoneContext _context;

        public BaseRepository(SellPhoneContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Get(TKey id)
        {
            return await _context.Set<T>().Where(x => x.Deleted == false && x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public virtual async Task<T> Find(Expression<Func<T, bool>> filters)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(filters);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filters)
        {
            return await _context.Set<T>().Where(filters).ToListAsync();
        }

        public virtual async Task Add(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
        }

        public virtual async Task AddRange(IEnumerable<T> objects)
        {
            await _context.Set<T>().AddRangeAsync(objects);
        }

        public virtual async Task Update(T obj)
        {
            try
            {
                _context.Set<T>().Update(obj);
            }
            catch (Exception ex)
            {

            }
        }

        public virtual async Task UpdateRange(IEnumerable<T> objects)
        {
            _context.Set<T>().UpdateRange(objects);
        }
        public virtual async Task Delete(T obj)
        {
            SoftDelete(obj);
        }

        private void SoftDelete(T obj)
        {
            obj.Deleted = true;
            obj.DeletedAt = DateTime.Now;
            _context.Set<T>().Update(obj);
        }

        private void HardDelete(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public virtual async Task DeleteRange(IEnumerable<T> objects)
        {
            foreach (var entity in objects)
            {
                entity.Deleted = true;
                entity.DeletedAt = DateTime.Now;
                _context.Set<T>().Update(entity);
            }
        }

    }
}
