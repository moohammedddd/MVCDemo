using DataAccessLayer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositiories
{
    public class DepartmentRepository(AppDbContext dbContext) : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext = dbContext;



        // CRUD operation 
        //get all
        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            //var departments = _dbContext.Departments.ToList();
            if (withTracking)
                return _dbContext.Departments.ToList();
            else
                return _dbContext.Departments.AsNoTracking().ToList();


        }

        //get ById
        public Department? GetById(int id)
        {
            var department = _dbContext.Departments.Find(id);
            return department;
        }


        //add 
        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

        //edit 
        public int Edit(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }
        //Delete
        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();

        }


    }
}
