using LogisticsApi.Dtos;
using LogisticsApi.Model;

namespace LogisticsApi.Services
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<AppUser>> GetEmployees();

        Task<AppUser> AddEmployee(AddUserDto model);
        Task<AppUser> GetEmployeeById(string id);
        Task<bool> DeleteEmployeeById(string id);
        Task<long> GetEmployeesCount();
    }
}