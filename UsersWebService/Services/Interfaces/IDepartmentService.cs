using UsersWebService.Models;

namespace UsersWebService.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentByIdAsync(long id);
    }
}
