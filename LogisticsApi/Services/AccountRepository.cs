using LogisticsApi.Dtos;
using LogisticsApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LogisticsApi.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<AppUser> _userManager;

        public AccountRepository(IOptions<AppSettings> appSettings, UserManager<AppUser> userManager)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }

        public LoginResponseDto Authenticate(SignInRequertDto model, AppUser user,string userRole)
        {
            //var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            // authentication successful so generate jwt token
            var token = generateJwtToken(user);
           
            return new LoginResponseDto(user, token,userRole);
        }

        public IEnumerable<AppUser> GetAllUsers()
        {
            return _userManager.Users;
        }

        //public async Task<IEnumerable<AppUser>> GetCustomers()
        //{
        //    return  await _userManager.GetUsersInRoleAsync("customer");
        //}
        //public async Task<IEnumerable<AppUser>> GetEmployees()
        //{
        //    return await _userManager.GetUsersInRoleAsync("employee");
        //}
        public async Task<AppUser> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        private string generateJwtToken(AppUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var signIn = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);//HmacSha256Signature

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = signIn
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
