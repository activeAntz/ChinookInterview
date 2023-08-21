using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Core.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbSet<TEntity> _dbSet;
        
        public Repository(DbContext context)
        {
            this._dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }
        public TEntity Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IList<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public int Count()
        {
           return _dbSet.Count();
        }
    }
}
