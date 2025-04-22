using DataAccessLayer.Migrations;
using Demo.BusinessLogic.DTOs.DepartmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Demo.BusinessLogic.Factory
{
    public static class DepartmentFactory
    {
        //Mapping Extension Method
        public static DepartmentDto ToDepartmentDto(this DataAccessLayer.Models.Department.Department department)
        {
            return new DepartmentDto()
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };
        }


        public static DepartmentDetailsDto ToDepartmentDetailsDto(this DataAccessLayer.Models.Department.Department department)
        {
            return new DepartmentDetailsDto()
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn
            };
        }
        
        public static DataAccessLayer.Models.Department.Department ToEntity(this CreateDepartmentDto dto)
        {
            return new DataAccessLayer.Models.Department.Department()
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                CreatedOn = DateTime.Now,
                CreatedBy = 1
            };
        }

        public static DataAccessLayer.Models.Department.Department ToEntity(this UpdateDepartmentDto dto)
        {
            return new DataAccessLayer.Models.Department.Department()
            {
                Id = dto.Id,
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                CreatedOn = dto.DateOfCreation
            };
        }

    }
}
