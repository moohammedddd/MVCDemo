using DataAccessLayer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositiories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity Entity);
        void Delete(TEntity Entity);
        void Edit(TEntity Entity);
        IEnumerable<TEntity> GetAll( bool withTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity,TResult>> selector);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>> filter);
        TEntity? GetById(int id);

        //IEnumerable<TEntity> GetIEnumerable();
        //IQueryable<TEntity> GetQueryable();
    }
}
