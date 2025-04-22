using DataAccessLayer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Department
{
    public class Department : BaseEntity
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Description { get; set; }


    }
}
