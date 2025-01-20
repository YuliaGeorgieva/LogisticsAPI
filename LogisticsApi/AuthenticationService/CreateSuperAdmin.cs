using LogisticsApi.Model;
using Microsoft.AspNetCore.Identity;

namespace LogisticsApi.AuthenticationService
{
    public class CreateSuperAdmin
    {
        public async Task CreateSuperAdminFunc(IServiceProvider serviceProvider)
        {
            var services = serviceProvider.CreateScope().ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("app");

            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

                logger.LogInformation("Start Seeding default data");
                await DefaultRoles.SeedAsync(roleManager);
                await DefaultUsers.SeedSuperAdminAsync(userManager);

                logger.LogInformation("Finished Seeding default data");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred seeding the DB");
            }
        }
    }
}
