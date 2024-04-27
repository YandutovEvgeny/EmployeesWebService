using UsersWebService.Models;

namespace UsersWebService.Services.Interfaces
{
    public interface IPassportService
    {
        Task<Passport> GetPassportByIdAsync(long id);
    }
}
