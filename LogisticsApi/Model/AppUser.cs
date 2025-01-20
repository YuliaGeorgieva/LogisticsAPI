using Microsoft.AspNetCore.Identity;

namespace LogisticsApi.Model
{
    public class AppUser : IdentityUser
    {
        public int? BranchId { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}