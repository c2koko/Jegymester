using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jegymester.DataContext.Dtos;
using Jegymester.Services;

namespace Jegymester.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto userDto)
        {
            var user = await _userService.RegisterUserAsync(userDto);
            return CreatedAtAction(nameof(RegisterUser), new { id = user.Id }, user);
        }



        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userDto)
        {
            var user = await _userService.UpdateUserAsync(id, userDto);
            return Ok(user);
        }
    }
}
