using Microsoft.AspNetCore.Mvc;
using UsersWebService.DataContracts;
using UsersWebService.Exceptions;
using UsersWebService.Models;
using UsersWebService.Services.Interfaces;

namespace UsersWebService.Controllers
{
    [Route("api/Employees/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<long> CreateEmployee(CreateEmployeeRequest request)
        {
            return await _employeeService.CreateEmployeeAsync(request);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee(long id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{companyId}")]
        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyId(long companyId)
        {
            return await _employeeService.GetEmployeesByCompanyIdAsync(companyId);
        }

        [HttpGet("{departmentId}")]
        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentId(long departmentId)
        {
            return await _employeeService.GetEmployeesByDepartmentIdAsync(departmentId);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(UpdateEmployeeRequest request)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(request);
                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
