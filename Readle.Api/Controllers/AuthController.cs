using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Readle.Application.Interface;
using Readle.Domain.Dtos;
using Readle.Domain.Entities;

namespace Readle.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<User>> RegisterAsync(UserDto user)
        {
            var User = await authServices.RegisterAsync(user);
           if(User is null)
            {
                return BadRequest("User Already Taken");
            }
            return Ok(User);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<TokenResponseDto>> LoginAsync( UserDto user)
        {
            var User = await authServices.LoginAsync(user);
            if (User is null)
            {
                return BadRequest("Invalid Username or Password");
            }
            return Ok(User);
        }
        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResult<bool>> LogoutAsync( )
        {
            var find = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (find is null || !Guid.TryParse(find.Value, out var Results))
            {
                return BadRequest("something went wrong");
            }
            var logout = await authServices.LogoutAsync(Results);
            if (!logout)
            {
                return BadRequest("something went wrong");
            }
            return Ok( new { message = "Logout Succesfully"});
        }
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<TokenResponseDto>> RefreshTokenAsync(User user)
        {
            var find = await authServices.RefreshTokenAsync(user);
            if(find is null || find.RefreshToken is null || user.RefreshToken is null)
            {
                return BadRequest("Unauthorized User");
            }
            return Ok(find);

        }

    }
}
