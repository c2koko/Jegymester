using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetpincerApp.DataContext.Dtos;

namespace Jegymester.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   
    [Authorize]
    public class UserControllers : ControllerBase
    {
        //private readonly IUserService _userService;



        [HttpPost("register")]
        [AllowAnonymous]

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            var token = await
        }


    }
}
