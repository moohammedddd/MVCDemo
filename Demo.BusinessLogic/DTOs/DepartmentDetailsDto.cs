using DataAccessLayer.Migrations;
using DataAccessLayer.Models;

namespace Demo.BusinessLogic.DTOs
{
   public  class DepartmentDetailsDto
    {

        // Mapping Constructor
        public DepartmentDetailsDto(DataAccessLayer.Models.Department department)
        {
            DeptId = department.Id;
            Name = department.Name;
            Code = department.Code;
            Description = department.Description;
            DateOfCreation = department.CreatedOn;
            CreatedBy = department.CreatedBy;
            LastModifiedBy = department.LastModifiedBy;
            LastModifiedOn = department.LastModifiedOn;

        }

        public DepartmentDetailsDto() { }


        public int DeptId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
