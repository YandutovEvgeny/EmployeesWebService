using Dapper;
using Microsoft.Data.SqlClient;
using UsersWebService.Exceptions;
using UsersWebService.Models;
using UsersWebService.Repositories.Interfaces;

namespace UsersWebService.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;
        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateDepartmentAsync(Department department)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var query = @"INSERT INTO [dbo].[Departments] ([Name], [Phone]) 
                                   OUTPUT INSERTED.[Id]
                                   VALUES (@Name, @Phone)";
                return (await connection.QueryAsync<int>(query, new
                {
                    Name = department.Name,
                    Phone = department.Phone
                })).FirstOrDefault();
            }
        }

        public async Task<Department> GetDepartmentByIdAsync(long departmentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [dbo].[Departments] WHERE Id = @DepartmentId";
                return (await connection.QueryAsync<Department>(query, new
                {
                    DepartmentId = departmentId
                })).FirstOrDefault() ?? throw new ObjectNotFoundException(departmentId, nameof(Department));
            }
        }
    }
}
