using Microsoft.EntityFrameworkCore;
using Product.Data.DAL;
using Product.Repositiries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Repositiries.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            var query = GetQuery(includes);
            return exp is null
                ? await query.FirstOrDefaultAsync()
                : await query.Where(exp).FirstOrDefaultAsync();
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {

            var query = GetQuery(includes);
            return exp is null
                ? await query.ToListAsync()
                : await query.Where(exp).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            var query = GetQuery(includes);
            return exp is null
                ? await query.Skip((page-1)*size).Take(8).ToListAsync()
                : await query.Where(exp).Skip((page - 1) * size).Take(8).ToListAsync();
        }

        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> exp)
        {
            return await _context.Set<TEntity>().AnyAsync(exp);
        }

        public async Task<int> TotalCount(Expression<Func<TEntity, bool>> exp = null)
        {
            return exp is null
               ? await _context.Set<TEntity>().CountAsync()
               : await _context.Set<TEntity>().Where(exp).CountAsync();
        }
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void Update(TEntity entity)
        {
             _context.Set<TEntity>().Update(entity);
        }
        private IQueryable <TEntity> GetQuery(params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query.Include(include);
                }
            }
            return query;
        }
    }
}
