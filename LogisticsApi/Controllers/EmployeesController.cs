using LogisticsApi.Dtos;
using LogisticsApi.Model;
using LogisticsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApi.Controllers
{
    //   [Authorize(Roles = "superadmin")]
    [Authorize]
      [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] AddUserDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseData { Status = false, Message = "Employee already exists!" });

            AppUser user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                BranchId = model.BranchId,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                City = model.City,
                State = model.State,
                Country = model.Country,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var isRoleAssigned = await _userManager.AddToRoleAsync(user, "employee");
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
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateUserDto model)
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
                       Message = $"Employee updation failed! {string.Join(", ", result.Errors.Select(e => e.Description))}"
                   });
                }
            }

            return Ok(new ResponseData { Status = true, Message = "Employee updated successfully!" });
        }

        [HttpGet]
        [Route("GetEmployeesCount")]
        public async Task<IActionResult> GetEmployeesCount()
        {
            var totalEmployees = await _employeeRepository.GetEmployeesCount();
            return Ok(totalEmployees);
        }

        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            return Ok(employee);
        }


        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var employee = await _employeeRepository.DeleteEmployeeById(id);
            return Ok(employee);
        }
    }
}