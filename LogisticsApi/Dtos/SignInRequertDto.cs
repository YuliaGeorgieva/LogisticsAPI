using System.ComponentModel.DataAnnotations;

namespace LogisticsApi.Dtos
{
    public class SignInRequertDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
