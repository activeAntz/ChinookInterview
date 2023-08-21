using System.Linq.Expressions;

namespace Chinook.Core.Infrastructure.Repositories
{
    public interface IRepository<TEntity> 
    {
        int Count();
        TEntity Get(int id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> GetAsync(int id);

        IList<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate = null);
        Task<IEnumerable<TEntity>> GetAllAsync();

        void Add(TEntity entity);

        void Remove(TEntity entity);
    }
}