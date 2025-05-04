using DataAccessLayer.Contexts;
using DataAccessLayer.Models.Shared;
using DataAccessLayer.Repositiories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositiories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext):IGenericRepository<TEntity> where TEntity: BaseEntity
    {

        // CRUD operation 
        //get all
        #region Get methods

  
        public IEnumerable<TEntity> GetAll( bool withTracking = false)
        {
            //var Set<TEntity> = _dbContext.Set<TEntity>.ToList();
            if (withTracking)
                return _dbContext.Set<TEntity>().Where(e => e.IsDeleted != true).ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();


        }


        //filter to make search 
        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _dbContext.Set<TEntity>()
                .Select(selector)
                .ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {

            return _dbContext.Set<TEntity>()
                .Where(e => e.IsDeleted != true)
                .Where(filter)
                .ToList();
        }

        //get ById
        public TEntity? GetById(int id)
        {
            var Employee = _dbContext.Set<TEntity>().Find(id);
            return Employee;
        }

        #endregion
        //add 
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            
        }

        //edit 
        public void Edit(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
       
        }
        //Delete
        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
           

        }

        
    

        //IEnumerable<TEntity> IGenericRepository<TEntity>.GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        //{
        //    throw new NotImplementedException();
        //}


        //public IEnumerable<TEntity> GetIEnumerable()
        //{
        //    return _dbContext.Set<TEntity>().ToList();
        //}

        //public IQueryable<TEntity> GetQueryable()
        //{
        //    return _dbContext.Set<TEntity>().AsQueryable();
        //}
    }
}
