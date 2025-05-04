using AutoMapper;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Repositiories.Interfaces;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services.AttchmentServices;
using Demo.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeServices(IUnitOfWork _unitOfWork , IMapper _mapper ,
        IAttchmentServices _attchmentServices ) : IEmployeeServices
    {


        public IEnumerable<GetEmployeeDto> GetAllEmployees(string? EmployeeSearchByName)
        {
            //var employee = _unitOfWork .EmployeeRepository.GetAll(e => new GetEmployeeDto()
            //{
            //    Id = e.Id,
            //    Name = e.Name,
            //    Email = e.Email,
            //    Age = e.Age,
            //});
            //return employee;
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
            IEnumerable<Employee> employees;
            if(string.IsNullOrEmpty(EmployeeSearchByName))
            
                 employees = _unitOfWork .EmployeeRepository.GetAll();
            else
            

             employees = _unitOfWork .EmployeeRepository.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchByName.ToLower())); // using .contain to make search match more fast 
            var employeesDtos = _mapper.Map<IEnumerable<GetEmployeeDto>>(employees);
            return employeesDtos;
            //var res = _unitOfWork .EmployeeRepository.GetIEnumerable() // local sequence
            //    .Select(emp => new GetEmployeeDto()
            //    {
            //        Id = emp.Id,
            //        Name = emp.Name,
            //        Email = emp.Email,
            //        Age = emp.Age,
            //    });

            //return res;
            //var res = _unitOfWork .EmployeeRepository.GetQueryable()// local sequence
            //  .Select(emp => new GetEmployeeDto()
            //  {
            //      Id = emp.Id,
            //      Name = emp.Name,
            //      Email = emp.Email,
            //      Age = emp.Age,
            //  }).ToList();

            //return res;


        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var emp = _unitOfWork .EmployeeRepository.GetById(id);
            if (emp is null) return null;
            //Make Profiles for active the automapping 
            return _mapper.Map<EmployeeDetailsDto>(emp);

        }

        public int? CreateEmployee(CreateEmployeeDto creatEmployeetDto)
        {
            // Call Attchment Service to Upload Employee Image 
            var mappedEmployee = _mapper.Map<Employee>(creatEmployeetDto);
            var imgName = _attchmentServices.Upload(creatEmployeetDto.Image, "Images");
            mappedEmployee.ImageName = imgName;
            _unitOfWork .EmployeeRepository.Add(mappedEmployee);
            return _unitOfWork.SaveChanges();
        }

     
        public int? UpdateEmployeeDto(UpdateEmployeeDto updateEmployeeDto)
        {
            var mappedEmployee = _mapper.Map<Employee>(updateEmployeeDto);
            _unitOfWork .EmployeeRepository.Edit(mappedEmployee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork .EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                 _unitOfWork .EmployeeRepository.Delete(employee);
                return _unitOfWork.SaveChanges()  > 0 ? true : false;
              
            }
        }
   
      public bool CreatePurchase()
        {
            //Purchase => insert Record | Done
            //Product => Update Quantities | Fail
            //Stores => Update Quantities| Done
            // Summary We Can Say Save Changes it should be in business layer


            //Save changes
             _unitOfWork.SaveChanges();
            return true;
        }
        public bool DeleteSale()
        {
            return true;
        }
    
    
    }
    }

