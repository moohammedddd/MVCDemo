
namespace DataAccessLayer.Repositiories
{
    public interface IDepartmentRepository
    {
        int Add(Department department);
        int Delete(Department department);
        int Edit(Department department);
        IEnumerable<Department> GetAll(bool withTracking = false);
        Department? GetById(int id);
    }
}