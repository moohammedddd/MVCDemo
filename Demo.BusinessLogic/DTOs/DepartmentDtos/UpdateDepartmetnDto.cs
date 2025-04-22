using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DTOs.DepartmentDtos
{
    public class UpdateDepartmentDto

    {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        public string Code { get; set; } = null;
        public string Description { get; set; } = null;
        public DateTime? DateOfCreation { get; set; } = null;
    }
}
