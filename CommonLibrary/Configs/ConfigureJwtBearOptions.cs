using CommonLibrary.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CommonLibrary.Configs
{
    public class ConfigureJwtBearOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IConfiguration configuration;

        public ConfigureJwtBearOptions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(string name, JwtBearerOptions options)
        {
            if (name == JwtBearerDefaults.AuthenticationScheme) // if is bearer token
            {
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                
                options.Authority = serviceSettings.Authority; 
                options.Audience = serviceSettings.ServiceName; // Setup Audience là Service cần auth và client id là postman
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role",
                };
            }
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(Options.DefaultName, options);
        }
    }
}
