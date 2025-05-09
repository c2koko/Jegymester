using Jegymester.DataContext.Dtos;
using Jegymester.Dtos;
using Jegymester.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jegymester.Controllers


    //FONTOS: Mivel nem tudtam ugyan abba a HttpPostba megírni mind a user createt és a ticketet,
    //ezért frontenden egy kattintásra kellene meghívni egymás után őket ebben a sorrendben: user create -> ticket create
    //és UGYAN AZT az email + phone adatot kell megkapniuk
{
    [Route("api/NotRegisteredUser")]
    [ApiController]
    public class NotRegisteredUserController : ControllerBase
    {
        private readonly INotRegisteredUserervice _userService;

        public NotRegisteredUserController(INotRegisteredUserervice userService)
        {
            _userService = userService;
        }

        [HttpPost("NotRegisteredUserCreate")]
        [AllowAnonymous]
        public async Task<IActionResult> NotRegisteredUserCreate([FromBody] NotRegisteredUserDto userDto, string _email, string _phone) 
        {
            var user = await _userService.CreateNotRegisteredUserAsync(userDto, _email, _phone);
            return Ok(user);
        }


        [HttpPost("NotRegisteredUserTicket")]
        [AllowAnonymous]
        public async Task<IActionResult> NotRegisteredUserTicket([FromBody] TicketCreateDto ticketDto, string _email, string _phone)
        {
            var ticket = await _userService.CreateTicketForNotRegisteredUserAsync(ticketDto, _email, _phone);
            return Ok(ticket);
        }
    }
}

