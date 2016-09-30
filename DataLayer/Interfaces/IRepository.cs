using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace AlbumProject.DataLayer.Interfaces
{
    public interface IRepository<TEntity>  where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
       // IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetSingle(string id);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        //Task<List<TEntity>> GetAllAsync();
       // Task<IEnumerable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        //Task<TEntity> GetSingleAsync(int id);
       // Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
