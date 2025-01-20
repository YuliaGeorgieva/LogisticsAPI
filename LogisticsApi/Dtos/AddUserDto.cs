using System.ComponentModel.DataAnnotations;

namespace LogisticsApi.Dtos
{
    public class AddUserDto
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public string Name { get; set; }
        public int BranchId { get; set; }
        public string? PhoneNumber { get; set; }
        
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}