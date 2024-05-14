using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MasterBackEnd.Identity.Domain;
using MasterBackEnd.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MasterBackEnd.Identity.Application.Interfaces;
using MasterBackEnd.Identity.Infrastructure.Services;
using System.Text;
using MasterBackEnd.Identity.Contracts;
using MasterBackEnd.Identity.Application.Queries.GetUserDetails;

namespace MasterBackEnd.Identity.Infrastructure.Startup
{
     public static class IdentityModuleStartup
     {
          public static IServiceCollection AddIdentityModule(this IServiceCollection services,
               IConfiguration configuration)
          {

               var connectionString = configuration.GetConnectionString("MasterDbContextConnection") 
                    ?? throw new InvalidOperationException("Connection string 'MasterDbContextConnection' not found.");

               services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));

               services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IdentityDbContext>();

              
               services.AddScoped<IUserService, GetUserDetailsQueryHandler>();

               services.Configure<IdentityOptions>(options =>
               {
                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // AppUser settings.
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = false;
               });

               services.AddAuthentication(options =>
               {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               }).AddJwtBearer(jwt =>
               {
                    var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
                    var issuer = configuration["Jwt:Issuer"];
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(key),
                         ValidateIssuer = true,
                         ValidIssuer = issuer,
                         ValidAudiences = new List<string>() { issuer },
                         ValidateAudience = true,
                         RequireExpirationTime = false,
                         ValidateLifetime = true
                    };
               });


               services.AddScoped<IJwtService, JwtService>();

               return services;
          }
     }
}
