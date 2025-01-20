using LogisticsApi.Model;
using Microsoft.AspNetCore.Identity;

namespace LogisticsApi.AuthenticationService
{
    public class DefaultUsers
    {
        public static async Task SeedSuperAdminAsync(UserManager<AppUser> userManager)
        {
            var checkUser = await userManager.FindByEmailAsync("admin@admin.com");
            if (checkUser != null)
                return;

            var defaultUser = new AppUser
            {
                UserName = "SuperAdmin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                BranchId = 1,
                Name= "SuperAdmin"
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                try
                {
                    var user = await userManager.FindByEmailAsync(defaultUser.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultUser, "admin@123");
                        await userManager.AddToRoleAsync(defaultUser, "superadmin");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
