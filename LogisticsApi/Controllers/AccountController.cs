using LogisticsApi.Dtos;
using LogisticsApi.Model;
using LogisticsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _accountRepository;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAccountRepository accountRepository, UserManager<AppUser> userManager)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(SignInRequertDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
            {
                return NotFound($"Unable to load user with username '{model.Username}'.");
            }

            var userRole = await _userManager.GetRolesAsync(user);
            var response = _accountRepository.Authenticate(model, user, userRole.First());

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseData { Status = false, Message = "User already exists!" });

            AppUser user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Email.Split('@')[0],
                IsDeleted=false,
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

            return Ok(new ResponseData { Status = true, Message = "User created successfully!" });
        }


        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAll()
        {
            var users = _accountRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUsersByRole")]
        public async Task<IActionResult> GetUserByRole(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);
            return Ok(users);
        }
    }
}
