using UsersWebService.DataContracts;
using UsersWebService.Models;

namespace UsersWebService.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<long> CreateEmployeeAsync(CreateEmployeeRequest request);
        Task DeleteEmployeeAsync(long id);
        Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync(long departmentId);
        Task UpdateEmployeeAsync(UpdateEmployeeRequest request);
    }
}
