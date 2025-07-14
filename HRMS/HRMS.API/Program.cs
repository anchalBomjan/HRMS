
using FluentValidation;
using HRMS.Application.Commands.Role.Create;
using HRMS.Application.Commands.User.Create;
using HRMS.Application.Common.Behaviors;
using HRMS.Application.Common.Interfaces;
using HRMS.Infrastructure;
using HRMS.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace HRMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

           
            //For authentication
            
            //Consolidation infrastructure and JWT configuration
            builder.Services.AddInfrastructure(builder.Configuration);
           
            // Add enum string conversion

            builder.Services.AddControllers()
              .AddJsonOptions(options =>
              {
              options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
               });

            // MediatR Configuration
            builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);


            // by default handler and validators assign transient by default
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(CreateRoleCommandHandler).Assembly);

            });
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



            // CORS Configuration
            builder.Services.AddCors(c =>
                c.AddPolicy("CorsPolicy", options =>
                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("ApiPolicy", policy =>
            //    {
            //        policy.AllowAnyOrigin()
            //              .AllowAnyMethod()
            //              .AllowAnyHeader()
            //              .WithExposedHeaders("X-Total-Count"); // For pagination metadata
            //    });
            //});


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
            app.UseCors("CorsPolicy");
            app.UseMiddleware<HRMS.API.MiddleWare.ExceptionHandlingMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
