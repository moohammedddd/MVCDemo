using AutoMapper;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Repositiories.Interfaces;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeServices(IEmployeeRepository _employeeRepository , IMapper _mapper) : IEmployeeServices
    {
       

        public IEnumerable<GetEmployeeDto> GetAllDepartment()
        {
            var employees = _employeeRepository.GetAll();
            // Map from IEnumerable<Employee> to IEnumerable<GetAllEmployeeDto>
            //var employeeDtos = employees.Select(emp => new GetEmployeeDto
            //{
            //    Id = emp.Id,
            //    Name = emp.Name,
            //    Email = emp.Email,
            //    Age = emp.Age,
            //    Salary = emp.Salary,
            //    IsActive = emp.IsActive,
            //    EmployeeType = emp.EmployeeType.ToString(),
            //    Gender = emp.Gender.ToString()
            //});

            //After Using AutoMapper 
            // Return TDestination(Mapped Object)
            // IMapper.Map<TDestination>(TSource)
            // IMapper.Map<TSource , TDestination>(TSource)
            
            var employeesDtos = _mapper.Map<IEnumerable<GetEmployeeDto>>(employees);
            return  employeesDtos;


        }

        public EmployeeDetailsDto? GetDepartmentById(int id)
        {
            var emp = _employeeRepository.GetById(id);
            if (emp is null) return null;
            //Make Profiles for active the automapping 
            return _mapper.Map<EmployeeDetailsDto>(emp);

        }

        public int? CreateEmployee(CreateEmployeeDto creatEmployeetDto)
        {
            var mappedEmployee = _mapper.Map<Employee>(creatEmployeetDto);
            var res = _employeeRepository.Add(mappedEmployee);
            return res;
        }

     
        public int? UpdateEmployeeDto(UpdateEmployeeDto updateEmployeeDto)
        {
            var mappedEmployee = _mapper.Map<Employee>(updateEmployeeDto);
            var res = _employeeRepository.Edit(mappedEmployee);
            return res;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                var res = _employeeRepository.Delete(employee);
                if (res > 0) return true;
                else return false;
            }
        }
    }
    }

