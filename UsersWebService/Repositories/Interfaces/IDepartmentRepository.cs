using UsersWebService.Models;

namespace UsersWebService.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<int> CreateDepartmentAsync(Department department);
        Task<Department> GetDepartmentByIdAsync(long id);
    }
}
