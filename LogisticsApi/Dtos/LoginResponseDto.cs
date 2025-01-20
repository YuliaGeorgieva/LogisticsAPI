using LogisticsApi.Model;

namespace LogisticsApi.Dtos
{
    public class LoginResponseDto
    {
        public string Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

        public LoginResponseDto(AppUser user, string token, string role)
        {
            Id = user.Id;
            Username = user.UserName;
            Token = token;
            Role = role;
        }
    }
}