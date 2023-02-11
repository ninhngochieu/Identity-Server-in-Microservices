using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityMicroservices.Data;
using IdentityMicroservices.Areas.Identity.Data;
using IdentityMicroservices.Settings;
using IdentityMicroservices.HostedServices;

namespace IdentityMicroservices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("IdentityMicroservicesContextConnection") ??
                throw new InvalidOperationException(
                    "Connection string 'IdentityMicroservicesContextConnection' not found.");

            builder.Services.AddDbContext<IdentityMicroservicesContext>(options => options.UseSqlite(connectionString));

            //builder.Services
            //    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<IdentityMicroservicesContext>();

            builder.Services
                .Configure<IdentitySettings>(builder.Configuration.GetSection(nameof(IdentitySettings))) //Config for Options
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<IdentityMicroservicesContext>();

            var identitySettings = builder.Configuration
                .GetSection(nameof(IdentityServerSettings))
                .Get<IdentityServerSettings>();

            builder.Services
                .AddIdentityServer(
                    opt =>
                    {
                        //Show log 
                        opt.Events.RaiseSuccessEvents = true;
                        opt.Events.RaiseFailureEvents = true;
                        opt.Events.RaiseErrorEvents = true;
                    })
                .AddInMemoryApiScopes(identitySettings.ApiScopes)
                .AddInMemoryClients(identitySettings.Clients)
                .AddInMemoryIdentityResources(identitySettings.IdentityResources)
                .AddInMemoryApiResources(identitySettings.ApiResources)
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Bảo vệ Api của Identity Server với scope là IdentityServerApi
            //Identity Server không có cơ chế tự bảo vệ. Đây là cách tự Auth bản thân nó
            builder.Services.AddLocalApiAuthentication(); 
            builder.Services.AddHostedService<IdentitySeedHostedService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.MapControllers();

            app.MapRazorPages();

            app.Run();
        }
    }
}