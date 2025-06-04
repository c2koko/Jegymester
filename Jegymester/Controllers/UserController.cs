using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jegymester.DataContext.Dtos;
using Jegymester.Services;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Jegymester.Controllers
{
    [Route("api/User")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("RegisterUser")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto userDto)
        {
            var user = await _userService.RegisterUserAsync(userDto);
            return CreatedAtAction(nameof(RegisterUser), new { id = user.Id }, user);
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userDto)
        {
            try
            {
                string token = await _userService.LoginUserAsync(userDto);
                return Ok(new { Token = token});
            }
            catch (UnauthorizedAccessException ex)
            {
                return NotFound("Invalid credentials or user doesn't exists");
            }
        }


        [HttpPut("UpdateUser")]
        [Authorize(Roles = "RegisteredUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userDto)
        {
            int userId = int.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            var user = await _userService.UpdateUserAsync(userId, userDto);
            return Ok(user);
        }

        [HttpGet("GetUserInfo/{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserInfo(int Id)
        {
            UserInfoDto dto = await _userService.GetUserInfoAsync(Id);
            if (dto == null)
            {
                return NotFound("User not found");
            }
            
            return Ok(dto);
        }

    }
}
