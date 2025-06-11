
using HRMS.Application.Commands.Role.Create;
using HRMS.Application.Commands.User.Create;
using HRMS.Application.Common.Interfaces;
using HRMS.Infrastructure;
using HRMS.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace HRMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //For authentication

            //Consolidation infrastructure and JWT configuration
            builder.Services.AddInfrastructure(builder.Configuration);

            // MediatR Configuration
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(CreateRoleCommandHandler).Assembly);

            });


            // CORS Configuration
            builder.Services.AddCors(c =>
                c.AddPolicy("CorsPolicy", options =>
                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
                });

                        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                         {
                           new OpenApiSecurityScheme
                           {
                              Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              }
                           },
                           Array.Empty<string>()
                         }
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
            app.UseCors("CorsPloicy");
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
