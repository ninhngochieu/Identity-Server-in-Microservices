using CommonLibrary.Configs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace CommonLibrary.Extensions
{
    public static class JwtBearerAuthenticationExtensions
    {
        public static AuthenticationBuilder AddJwtBearerAuthentication(this IServiceCollection services)
        {
            return services.ConfigureOptions<ConfigureJwtBearOptions>() // Các thông tin liên quan để Server Auth được cấu hình ở đây
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //Bearer token
                .AddJwtBearer(
                    //options =>
                    //{
                    //    options.Authority = "https://localhost:7012";
                    //    options.Audience = nameof(Services.CatalogServices);
                    //}
                    );
        }
    }
}
