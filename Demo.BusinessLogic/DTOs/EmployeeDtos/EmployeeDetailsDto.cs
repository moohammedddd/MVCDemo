﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DTOs.EmployeeDtos
{
     public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
     
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public int DepartmentId { get; set; }

        public string? ImageName { get; set; }
        public string? ImageDesc { get; set; }
        #region Member To Configure
        public DateOnly HiringDate { get; set; } //MisMatch
        public string Gender { get; set; }
        public string EmployeeType { get; set; }
        public string Department { get; set; } 
        #endregion
    }
}
