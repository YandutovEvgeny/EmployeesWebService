using Azure.Core;
using UsersWebService.DataContracts;
using UsersWebService.Exceptions;
using UsersWebService.Models;
using UsersWebService.Repositories.Interfaces;
using UsersWebService.Services.Interfaces;

namespace UsersWebService.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentService _departmentService;
        private readonly IPassportService _passportService;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentService departmentService, IPassportService passportService)
        {
            _employeeRepository = employeeRepository;
            _departmentService = departmentService;
            _passportService = passportService;
        }

        public async Task<long> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            //Проверки на наличие объектов в базе
            long? departmentId = await CheckDepartmentExistence(request.DepartmentId);
            long? passportId = await CheckPassportExistence(request.PassportId);

            return await _employeeRepository.CreateEmployeeAsync(request);
        }

        public async Task DeleteEmployeeAsync(long employeeId)
        {
            Employee employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee != null)
            {
                await _employeeRepository.DeleteEmployeeAsync(employeeId);
            }
        }

        public Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId)
        {
            return _employeeRepository.GetEmployeesByCompanyIdAsync(companyId);
        }

        public Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync(long departmentId)
        {
            return _employeeRepository.GetEmployeesByDepartmentIdAsync(departmentId);
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            //Проверки на наличие объектов в базе
            long? departmentId = await CheckDepartmentExistence(request.DepartmentId.Value);
            long? passportId = await CheckPassportExistence(request.PassportId.Value);

            Employee employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
            if(employee != null)
            {
                await _employeeRepository.UpdateEmployeeAsync(request);
            }
        }

        private async Task<long?> CheckDepartmentExistence(long? id)
        {
            try
            {
                if (id != null)
                {
                    return (await _departmentService.GetDepartmentByIdAsync(id.Value)).Id;
                }
            }
            catch (ObjectNotFoundException)
            {
                //Обрабатываем, если в базе нет объекта
            }

            return 0;
        }

        private async Task<long?> CheckPassportExistence(long? id)
        {
            try
            {
                if (id != null)
                {
                    return (await _passportService.GetPassportByIdAsync(id.Value)).Id;
                }
            }
            catch (ObjectNotFoundException)
            {
                //Обрабатываем, если в базе нет объекта
            }

            return 0;
        }
    }
}
