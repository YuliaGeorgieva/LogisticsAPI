using LogisticsApi.Dtos;
using LogisticsApi.Model;

namespace LogisticsApi.Services
{
    public interface IAccountRepository
    {
        LoginResponseDto Authenticate(SignInRequertDto model, AppUser user, string userRole);
        IEnumerable<AppUser> GetAllUsers();
        //Task<IEnumerable<AppUser>> GetCustomers();
        //Task<IEnumerable<AppUser>> GetEmployees();
        Task<AppUser> GetById(string id);
    }
}