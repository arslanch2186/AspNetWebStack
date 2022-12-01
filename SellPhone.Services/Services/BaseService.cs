using SellPhone.Db.Repositories;
using SellPhone.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SellPhone.Services.Services
{
    public class BaseService<T, TKey> : IBaseService<T, TKey> where T : SoftDeletedEntity<TKey>, new()
    {
        protected readonly IBaseRepository<T, TKey> _baseRepository;

        protected BaseService(IBaseRepository<T, TKey> repository)
        {
            _baseRepository = repository;
        }
        public virtual async Task<T> Get(TKey id)
        {
            return await _baseRepository.Get(id);
        }
        public virtual async Task<T> Find(Expression<Func<T, bool>> filters)
        {
            return await _baseRepository.Find(filters);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _baseRepository.GetAll();
        }
        public virtual async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filters)
        {
            return await _baseRepository.GetList(filters);
        }

        public virtual async Task Add(T obj)
        {
            await _baseRepository.Add(obj);
        }

        public virtual async Task AddRange(IEnumerable<T> objects)
        {
            await _baseRepository.AddRange(objects);
        }

        public virtual async Task Update(T obj)
        {
            try
            {
                await _baseRepository.Update(obj);
            }
            catch(Exception ex)
            {

            }
        }

        public virtual async Task UpdateRange(IEnumerable<T> objects)
        {
            await _baseRepository.UpdateRange(objects);
        }
        public virtual async Task Delete(TKey id)
        {
            var obj = await _baseRepository.Get(id);
            await _baseRepository.Delete(obj);
        }

        public virtual async Task DeleteRange(IEnumerable<T> objects)
        {
            await _baseRepository.DeleteRange(objects);
        }
    }
}
