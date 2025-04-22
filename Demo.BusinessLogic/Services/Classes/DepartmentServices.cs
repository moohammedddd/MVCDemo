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




namespace Demo.BusinessLogic.Services
{
    public class DepartmentServices(IDepartmentRepository _departmentRepository) : IDepartmentServices
    {
        // Get All Department 
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            var mdepartments = departments.Select(d => d.ToDepartmentDto());
            //{
            //    DeptId = d.Id,
            //    Name = d.Name,
            //    Code = d.Code,
            //    Description = d.Description,
            //    DateOfCreation = d.CreatedOn
            //});
            return mdepartments;
        }

        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
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
            var res = _departmentRepository.Add(departmentEntity);
            return res;
        }

        public int? UpdateDepartment(UpdateDepartmentDto updateDepartmetnDto)
        {
            var departmentEntity = updateDepartmetnDto.ToEntity();
            var res = _departmentRepository.Edit(departmentEntity);
            return res;
        }


        public  bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                var res = _departmentRepository.Delete(department);
                if (res > 0) return true;
                else return false;
            }
        }

        //Types of Mapping 
        //1 Manual Mapping 
        // 2 Auto Mapper
        //3 Constructor Mapping
        //4 Extension Methods 


    }
}