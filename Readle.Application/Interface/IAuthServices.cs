using Readle.Domain.Dtos;
using Readle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readle.Application.Interface
{
  public interface IAuthServices
    {
        Task<TokenResponseDto?> RefreshTokenAsync(User request);
        Task<User?> RegisterAsync(UserDto Request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<bool> LogoutAsync(Guid user);
    }
}
