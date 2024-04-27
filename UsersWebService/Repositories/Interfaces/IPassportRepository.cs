using UsersWebService.Models;

namespace UsersWebService.Repositories.Interfaces
{
    public interface IPassportRepository
    {
        Task<Passport> GetPassportByIdAsync(long id);
    }
}
