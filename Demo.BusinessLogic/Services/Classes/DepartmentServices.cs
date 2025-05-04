using DataAccessLayer.Migrations;
using DataAccessLayer.Repositiories.Interfaces;
using Demo.BusinessLogic.DTOs;
using Demo.BusinessLogic.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.DTOs.DepartmentDtos;
using DepartmentModel = DataAccessLayer.Models.Department.Department;
using AutoMapper;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Repositiories.Classes;



namespace Demo.BusinessLogic.Services
{
    public class DepartmentServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IDepartmentServices
    {
        // Get All Department 
        public IEnumerable<DepartmentDto> GetAllDepartments(string? DepartmentSearch)
        {
            IEnumerable<DepartmentModel> departments;

            if (string.IsNullOrEmpty(DepartmentSearch))
            {
                departments =  _unitOfWork.DepartmentRepository.GetAll();
            }
            else
            {
                departments =  _unitOfWork.DepartmentRepository.GetAll(e => e.Name.ToLower().Contains(DepartmentSearch.ToLower()));
            }

            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var department =  _unitOfWork.DepartmentRepository.GetById(id);
            if (department == null)
                return null;
            // var departmentToReturn = new DepartmentDetailsDto()
            //{
            //    DeptId = department.Id,
            //    Name = department.Name,
            //    Code = department.Code,
            //    Description = department.Description,
            //    DateOfCreation = department.CreatedOn,
            //    CreatedBy = department.CreatedBy,
            //    LastModifiedBy = department.LastModifiedOn
            //};
            return department.ToDepartmentDetailsDto();
        }


        // Add Departemt 
        public int? CreateDepartment(CreateDepartmentDto createDepartmantDto)
        {
            var departmentEntity = createDepartmantDto.ToEntity();
            _unitOfWork.DepartmentRepository.Add(departmentEntity);
            return _unitOfWork.SaveChanges();
        }

        public int? UpdateDepartment(UpdateDepartmentDto updateDepartmetnDto)
        {
            var departmentEntity = updateDepartmetnDto.ToEntity();
            _unitOfWork.DepartmentRepository.Edit(departmentEntity);
            return _unitOfWork.SaveChanges();
        }


        public  bool DeleteDepartment(int id)
        {
            var department =  _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
               _unitOfWork.DepartmentRepository.Delete(department);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

        //Types of Mapping 
        //1 Manual Mapping 
        // 2 Auto Mapper
        //3 Constructor Mapping
        //4 Extension Methods 


    }
}