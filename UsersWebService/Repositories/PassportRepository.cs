using Dapper;
using Microsoft.Data.SqlClient;
using UsersWebService.Exceptions;
using UsersWebService.Models;
using UsersWebService.Repositories.Interfaces;

namespace UsersWebService.Repositories
{
    public class PassportRepository : IPassportRepository
    {
        private readonly string _connectionString;

        public PassportRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Passport> GetPassportByIdAsync(long passportId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [dbo].[Passports] WHERE Id = @PassportId";
                return (await connection.QueryAsync<Passport>(query, new
                {
                    PassportId = passportId
                })).FirstOrDefault() ?? throw new ObjectNotFoundException(passportId, nameof(Passport));
            }
        }
    }
}
