using LogisticsApi.Dtos;
using LogisticsApi.Model;
using LogisticsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApi.Controllers
{
    //   [Authorize(Roles = "superadmin,employee")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository, UserManager<AppUser> userManager)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] AddUserDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseData { Status = false, Message = "Customer already exists!" });

            AppUser user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                City = model.City,
                State = model.State,
                Country = model.Country,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var isRoleAssigned = await _userManager.AddToRoleAsync(user, "customer");
                if (!isRoleAssigned.Succeeded)
                {
                    return StatusCode(
                   StatusCodes.Status500InternalServerError,
                   new ResponseData
                   {
                       Status = false,
                       Message = $"Customer creation failed! {string.Join(", ", result.Errors.Select(e => e.Description))}"
                   });
                }
            }

            return Ok(new ResponseData { Status = true, Message = "Customer added successfully!" });
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateUserDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                //update
                userExists.Email = model.Email;
                userExists.UserName = model.Email;
                userExists.Name = model.Name;
                userExists.PhoneNumber = model.PhoneNumber;
                userExists.Address = model.Address;
                userExists.City = model.City;
                userExists.State = model.State;
                userExists.Country = model.Country;

                var result = await _userManager.UpdateAsync(userExists);
                if (!result.Succeeded)
                {
                    return StatusCode(
                   StatusCodes.Status500InternalServerError,
                   new ResponseData
                   {
                       Status = false,
                       Message = $"Customer updation failed! {string.Join(", ", result.Errors.Select(e => e.Description))}"
                   });
                }
            }

            return Ok(new ResponseData { Status = true, Message = "Customer updated successfully!" });
        }


        [HttpGet]
        [Route("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomers();
            return Ok(customers);
        }
        [HttpGet]
        [Route("GetCustomersCount")]
        public async Task<IActionResult> GetCustomersCount()
        {
            var totalCustomers = await _customerRepository.GetCustomersCount();
            return Ok(totalCustomers);
        }

        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            return Ok(customer);
        }
        [HttpDelete]
        [Route("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customers = await _customerRepository.DeleteCustomerById(id);
            return Ok(customers);
        }

    }
}