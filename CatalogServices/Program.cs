using CommonLibrary.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CatalogServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddJwtBearerAuthentication();
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.Authority = "https://localhost:7012";
            //    options.Audience = nameof(CatalogServices);
            //});
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy(Policies.Read, p =>
                {
                    //Cấu hình phải là admin
                    p.RequireRole("Admin"); 
                    //Quyền truy cập lên tài nguyên phải có
                    p.RequireClaim("scope","catalog.readaccess","catalog.fullaccess");
                });

                opt.AddPolicy(Policies.Write, p =>
                {
                    p.RequireRole("Admin");
                    p.RequireClaim("scope", "catalog.writeaccess", "catalog.fullaccess");
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}