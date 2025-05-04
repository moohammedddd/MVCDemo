using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Contexts;
using DataAccessLayer.Repositiories.Interfaces;

namespace DataAccessLayer.Repositiories.Classes
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository1;
        private readonly AppDbContext appContext;

        public UnitOfWork (AppDbContext appContext)
        {
            this.appContext = appContext;
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(appContext));
            _departmentRepository1 = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(appContext));

        }



        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;
        public IDepartmentRepository DepartmentRepository => _departmentRepository1.Value;


        public int SaveChanges()
        {
            return appContext.SaveChanges();
        }
    }
}
