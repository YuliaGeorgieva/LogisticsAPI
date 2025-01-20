using LogisticsApi.Dtos;
using LogisticsApi.Model;

namespace LogisticsApi.Services
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<AppUser>> GetCustomers();

        //Task<AppUser> AddCustomer(AddUserDto model); 
        Task<AppUser> GetCustomerById(string id);
        Task<bool> DeleteCustomerById(string id);
        Task<long> GetCustomersCount();
    }
}