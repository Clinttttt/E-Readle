using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Readle.Application.Interface;
using Readle.Domain.Dtos;
using Readle.Domain.Entities;
using Readle.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Readle.Infrastructure.Services
{
  public class AuthServices(ApplicationDbContext context, IConfiguration configuration) : IAuthServices
    {
      public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var Key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512);

            var TokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            return  new JwtSecurityTokenHandler().WriteToken(TokenDescriptor);
       
        }
        public string GenerateRefreshToken()
        {
            var randomnumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomnumber);
            return Convert.ToBase64String(randomnumber);
        }
        public async Task<string> GenerateAndSaveRefreshToken(User user)
        {
            var refreshtoken = GenerateRefreshToken();
            user.RefreshToken = refreshtoken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshtoken;
        }
        public async Task<TokenResponseDto> CreateTokenResponse(User user)
        {
         return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)
            };
        
        }
        public async Task<User?> ValidateRefreshToken(Guid UserId, string RefreshToken)
        {
            var user = await context.Users.FindAsync(UserId);
            if(user is null || user.RefreshToken != RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }
        public async Task<TokenResponseDto?> RefreshTokenAsync(User request)
        {
            var user = await ValidateRefreshToken(request.Id, request.RefreshToken!);
            if(user is null)
            {
                return null;
            }
            return await CreateTokenResponse(user);
        }
        public async Task<User?> RegisterAsync(UserDto Request)
        {
            if (await context.Users.AnyAsync(u => u.UserName == Request.Username))
            {
                return null;
            }
            var user = new User();
            var HashPassword = new PasswordHasher<User>()
             .HashPassword(user, Request.Password!);
            user.UserName = Request.Username!;
            user.PasswordHash = HashPassword;
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        } 
        public async Task<TokenResponseDto?> LoginAsync(UserDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == request.Username);
                 if(user is null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password!) == PasswordVerificationResult.Failed){
                return null;
            }
            return await CreateTokenResponse(user);
        }
        public async Task<bool> LogoutAsync(Guid user)
        {
            var find = await context.Users.FindAsync(user);
            if (find is null)
            {
                return false;
            }
            find.RefreshToken = null;
            find.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(-1);
            await context.SaveChangesAsync();
            return true;
        }
    }

}
