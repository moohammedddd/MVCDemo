using DataAccessLayer.Contexts;
using DataAccessLayer.Repositiories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositiories.Classes
{
            public class EmployeeRepository(AppDbContext dbContext) :
             GenericRepository<Employee>(dbContext),
             IEmployeeRepository

            {





            }
}
