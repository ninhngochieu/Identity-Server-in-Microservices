using IdentityMicroservices.Areas.Identity.Data;
using IdentityMicroservices.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdentityMicroservices.HostedServices
{
    public class IdentitySeedHostedService : IHostedService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IdentitySettings setting;

        public IdentitySeedHostedService(IServiceScopeFactory serviceScopeFactory, IOptions<IdentitySettings> identitySettings)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            setting = identitySettings.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await CreateRoleIfNotExistsAsync(Roles.Admin, roleManager);
            await CreateRoleIfNotExistsAsync(Roles.Player, roleManager);

            var adminUser = await userManager.FindByEmailAsync(setting.AdminUserEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser()
                {
                    UserName = setting.AdminUserEmail,
                    Email = setting.AdminUserEmail
                };

                await userManager.CreateAsync(adminUser, setting.AdminUserPassword);
                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private static async Task CreateRoleIfNotExistsAsync(string role, RoleManager<ApplicationRole> roleManager)
        {
            bool existedRole = await roleManager.RoleExistsAsync(role);

            if (!existedRole)
            {
                await roleManager.CreateAsync(new ApplicationRole() { Name = role});
            }
        }
    }
}
