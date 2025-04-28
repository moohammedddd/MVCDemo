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
        int Add(TEntity Entity);
        int Delete(TEntity Entity);
        int Edit(TEntity Entity);
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity,TResult>> selector);
        TEntity? GetById(int id);

        //IEnumerable<TEntity> GetIEnumerable();
        //IQueryable<TEntity> GetQueryable();
    }
}
