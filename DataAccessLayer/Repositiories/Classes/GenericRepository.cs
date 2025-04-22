using DataAccessLayer.Contexts;
using DataAccessLayer.Models.Shared;
using DataAccessLayer.Repositiories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositiories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext):IGenericRepository<TEntity> where TEntity: BaseEntity
    {

        // CRUD operation 
        //get all
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            //var Set<TEntity> = _dbContext.Set<TEntity>.ToList();
            if (withTracking)
                return _dbContext.Set<TEntity>().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();


        }

        //get ById
        public TEntity? GetById(int id)
        {
            var Employee = _dbContext.Set<TEntity>().Find(id);
            return Employee;
        }


        //add 
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        //edit 
        public int Edit(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
        //Delete
        public int Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();

        }
    }
}
