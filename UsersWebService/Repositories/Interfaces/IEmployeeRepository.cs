using UsersWebService.DataContracts;
using UsersWebService.Models;

namespace UsersWebService.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<long> CreateEmployeeAsync(CreateEmployeeRequest request);
        Task DeleteEmployeeAsync(long employeeId);
        Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync(long departmentId);
        Task UpdateEmployeeAsync(UpdateEmployeeRequest request);
        Task<Employee> GetEmployeeByIdAsync(long employeeId);

    }
}
