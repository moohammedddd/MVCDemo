using Demo.BusinessLogic.DTOs.DepartmentDtos;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IDepartmentServices
    {
        int? CreateDepartment(CreateDepartmentDto createDepartmentDto);
        IEnumerable<DepartmentDto> GetAllDepartments(string? DepartmentSearch = null);
        DepartmentDetailsDto GetDepartmentById(int id);
        int? UpdateDepartment(UpdateDepartmentDto updateDepartmentDto);
        bool DeleteDepartment(int id);
    }
}