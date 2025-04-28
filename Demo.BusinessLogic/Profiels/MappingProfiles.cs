using AutoMapper;
using DataAccessLayer.Models.Employees;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Profiels
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            // CreateMap<TSource, TDestination>()
            CreateMap<Employee, GetEmployeeDto>()
             //ths process when the name of the property in table different in another tabla that i cast from to ...
             .ForMember(dest => dest.EmpType, options => options.MapFrom(emp => emp.EmployeeType)) 
    .ForMember(dest => dest.EmpGender, options => options.MapFrom(emp => emp.Gender))
                .ForMember(dest => dest.Department, options => options.MapFrom(emp => emp.Department != null ? emp.Department.Name : null)); //Get
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.EmployeeType, options => options.MapFrom(emp => emp.EmployeeType))
                .ForMember(dest => dest.Gender, options => options.MapFrom(emp => emp.Gender))
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(emp => DateOnly.FromDateTime(emp.HiringDate))) //Get
                .ForMember(dest => dest.Department, options => options.MapFrom(emp => emp.Department != null ? emp.Department.Name : null));
            // Reversed Because the method is Post 
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate , options => options.MapFrom(empDto => empDto.HiringDate.ToDateTime(TimeOnly.MinValue))); // Post
            CreateMap<UpdateEmployeeDto, Employee>()
                 .ForMember(dest => dest.HiringDate, options => options.MapFrom(empDto => empDto.HiringDate.ToDateTime(TimeOnly.MinValue)));// post


        }

      

     
    }
}
