using DataAccessLayer.Contexts;
using DataAccessLayer.Models.Department;
using DataAccessLayer.Repositiories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositiories.Classes
{
    public class DepartmentRepository(AppDbContext appDbContext) :
        GenericRepository<Department>(appDbContext),
        IDepartmentRepository
    {

    }
}
