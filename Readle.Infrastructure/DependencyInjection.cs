using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Readle.Application.Interface;
using Readle.Infrastructure.Data;
using Readle.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Readle.Infrastructure
{
   public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidIssuer = configuration["AppSettings:Issuer"],
                 ValidateAudience = true,
                 ValidAudience = configuration["AppSettings:Audience"],
                 ValidateLifetime = true,

                 IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]!)),
                 ValidateIssuerSigningKey = true,
                 NameClaimType = ClaimTypes.Name
             });
            services.AddScoped<IAuthServices, AuthServices>();

         
    
            
                  
            
            
            
            
            
            
            
            
            
            
            
            
            return services;
        }
    }
}
