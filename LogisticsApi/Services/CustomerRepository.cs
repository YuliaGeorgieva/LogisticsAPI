using LogisticsApi.Dtos;
using LogisticsApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LogisticsApi.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<AppUser> _userManager;

        public CustomerRepository(IOptions<AppSettings> appSettings, UserManager<AppUser> userManager)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppUser>> GetCustomers()
        {
            var allCustomers=  await _userManager.GetUsersInRoleAsync("customer");
            var response = allCustomers.Where(x=> !x.IsDeleted).ToList();
            return response;
        }
        public async Task<long> GetCustomersCount()
        {
            var allCustomers = await _userManager.GetUsersInRoleAsync("customer");
            return allCustomers.Where(x => !x.IsDeleted).LongCount();
        }

        //public async Task<AppUser> AddCustomer(AddUserDto model)
        //{
        //    AppUser user = new AppUser()
        //    {
        //        Email = model.Email,
        //        UserName = model.Email,
        //        Name = model.Name,
        //        BranchId = model.BranchId,
        //        PhoneNumber = model.PhoneNumber,
        //        Address = model.Address,
        //        City = model.City,
        //        State = model.State,
        //        Country = model.Country,
        //    };

        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    return user;
        //}

        public async Task<AppUser> GetCustomerById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> DeleteCustomerById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsDeleted = true;
            var result =await _userManager.UpdateAsync(user);
            if(result.Succeeded)
                return true;

            return false;
        }
    }
}