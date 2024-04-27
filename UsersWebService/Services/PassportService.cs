using UsersWebService.Models;
using UsersWebService.Repositories.Interfaces;
using UsersWebService.Services.Interfaces;

namespace UsersWebService.Services
{
    public class PassportService : IPassportService
    {
        private readonly IPassportRepository _passportRepository;

        public PassportService(IPassportRepository passportRepository)
        {
            _passportRepository = passportRepository;
        }

        public Task<Passport> GetPassportByIdAsync(long id)
        {
            return _passportRepository.GetPassportByIdAsync(id);
        }
    }
}
