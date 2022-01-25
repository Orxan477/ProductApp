using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Repositiries.Interfaces
{
    public interface IRepository<TEntity>

    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<List<TEntity>> GetAllPaginatedAsync(int page, int size,Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<int> TotalCount(Expression<Func<TEntity, bool>> exp = null);
        Task<bool> IsExist(Expression<Func<TEntity, bool>> exp);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
