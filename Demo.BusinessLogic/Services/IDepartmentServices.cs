using Demo.BusinessLogic.DTOs;

namespace Demo.BusinessLogic.Services
{
    public interface IDepartmentServices
    {
        int? CreateDepartment(CreateDepartmantDto createDepartmantDto);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto GetDepartmentById(int id);
        int? UpdateDepartment(UpdateDepartmetnDto updateDepartmetnDto);
    }
}