using UsersWebService.Models;
using UsersWebService.Repositories.Interfaces;
using UsersWebService.Services.Interfaces;

namespace UsersWebService.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<Department> GetDepartmentByIdAsync(long id)
        {
            return _departmentRepository.GetDepartmentByIdAsync(id);
        }
    }
}
