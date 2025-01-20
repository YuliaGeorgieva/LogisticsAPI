using Microsoft.AspNetCore.Identity;

namespace LogisticsApi.AuthenticationService
{
    public class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            var getAdminRole = await roleManager.FindByNameAsync("superadmin");
            if (getAdminRole == null)
                await roleManager.CreateAsync(new IdentityRole() { Name = "superadmin" });

            var getCustomerRole = await roleManager.FindByNameAsync("customer");
            if (getCustomerRole == null)
                await roleManager.CreateAsync(new IdentityRole() { Name = "customer" });

            var getEmployeeRole = await roleManager.FindByNameAsync("employee");
            if (getEmployeeRole == null)
                await roleManager.CreateAsync(new IdentityRole() { Name = "employee" });
        }
    }
}