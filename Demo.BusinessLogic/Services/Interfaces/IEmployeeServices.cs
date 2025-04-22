using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeServices
    {
        IEnumerable<GetEmployeeDto> GetAllDepartment();
        EmployeeDetailsDto GetDepartmentById(int id);
        int? UpdateEmployeeDto(UpdateEmployeeDto  updateEmployeeDto);
        bool DeleteEmployee(int id);
        int? CreateEmployee(CreateEmployeeDto creatEmployeetDto);


    }

}
