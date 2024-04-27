using Dapper;
using Microsoft.Data.SqlClient;
using UsersWebService.DataContracts;
using UsersWebService.Exceptions;
using UsersWebService.Models;
using UsersWebService.Repositories.Interfaces;

namespace UsersWebService.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<long> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"INSERT INTO [dbo].[Employees] ([Name], [Surname], [Phone], [CompanyId], [DepartmentId], [PassportId])
                                   OUTPUT INSERTED.[Id]
                                   VALUES (@Name, @Surname, @Phone, @CompanyId, @DepartmentId, @PassportId)";
                
                var response = (await connection.QueryAsync<long>(query, new
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Phone = request.Phone,
                    CompanyId = request.CompanyId,
                    DepartmentId = request.DepartmentId,
                    PassportId = request.PassportId
                })).FirstOrDefault();

                return response;
            }
        }

        public async Task DeleteEmployeeAsync(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var deleteQuery = "DELETE FROM [dbo].[Employees] WHERE Id = @id";
                await connection.ExecuteAsync(deleteQuery, new { id });
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(long employeeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var selectQuery = "SELECT * FROM [dbo].[Employees] WHERE Id = @Id";
                return await connection.QuerySingleOrDefaultAsync<Employee>(selectQuery, new { Id = employeeId }) ?? throw new ObjectNotFoundException(employeeId, nameof(Employee));
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyIdAsync(long companyId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var lookup = new Dictionary<int, Employee>();
                var query = @"SELECT e.*, d.*, p.* FROM [dbo].[Employees] as e
                                              LEFT JOIN [dbo].[Departments] as d ON e.DepartmentId = d.Id
                                              LEFT JOIN [dbo].[Passports] as p ON e.PassportId = p.Id
                                                  WHERE e.CompanyId = @CompanyId";
                return await connection.QueryAsync<Employee, Department, Passport, Employee>
                    (query, (e, d, p) =>
                    {
                        e.Department = d;
                        e.Passport = p;
                        return e;
                    }, new { CompanyId = companyId });
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync(long departmentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT * FROM [dbo].[Employees] as e
                                  LEFT JOIN [dbo].[Departments] as d ON e.DepartmentId = d.Id
                                  LEFT JOIN [dbo].[Passports] as p ON e.PassportId = p.Id
                                      WHERE DepartmentId = @DepartmentId";
                return await connection.QueryAsync<Employee, Department, Passport, Employee>
                    (query, (e, d, p) =>
                    {
                        e.Department = d;
                        e.Passport = p;
                        return e;
                    }, new { DepartmentId = departmentId });
            }
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"UPDATE [dbo].[Employees] SET ";

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query += "Name = @Name, ";
                }

                if (!string.IsNullOrEmpty(request.Surname))
                {
                    query += "Surname = @Surname, ";
                }

                if (!string.IsNullOrEmpty(request.Phone))
                {
                    query += "Phone = @Phone, ";
                }

                if(request.CompanyId != null && request.CompanyId.Value != 0)
                {
                    query += "CompanyId = @CompanyId, ";
                }

                if(request.PassportId != null && request.PassportId.Value != 0)
                {
                    query += "PassportId = @PassportId, ";
                }

                if(request.DepartmentId != null && request.DepartmentId.Value != 0)
                {
                    query += "DepartmentId = @DepartmentId, ";
                }

                query = query.TrimEnd(',', ' ');
                query += " WHERE Id = @Id";

                await connection.ExecuteAsync(query, request);
            }
        }
    }
}
