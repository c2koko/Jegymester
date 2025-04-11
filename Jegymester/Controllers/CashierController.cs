using Jegymester.DataContext.Dtos;
using Jegymester.Dtos;
using Jegymester.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jegymester.Controllers
{
    [Route("api/Cashier")]
    [ApiController]
    [Authorize(Roles = "Cashier")]
    public class CashierController : ControllerBase
    {
        private readonly ICashierService _cashierService;

        public CashierController(ICashierService cashierService)
        {
            _cashierService = cashierService;
        }

        [HttpPut("VerifyTicket/{id}")]
        public async Task<IActionResult> VerifyTicket(int id, [FromBody] TicketVerifyDto ticketDto)
        {
            var ticket = await _cashierService.VerifyTicketAsync(id, ticketDto);
            return Ok(ticket);
        }
    }
}
