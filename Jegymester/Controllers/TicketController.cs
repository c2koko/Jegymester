using Microsoft.AspNetCore.Mvc;
using Jegymester.DataContext.Data;
using Microsoft.EntityFrameworkCore;
using Jegymester.DataContext.Entities;
using Jegymester.Services;
using Jegymester.Dtos;
using Jegymester.DataContext.Dtos;

namespace Jegymester.Controllers
{
    [Route("api/Ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }


        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDto ticketDto)
        {
            var ticket = await _ticketService.CreateTicketAsync(ticketDto);
            return CreatedAtAction(nameof(CreateTicket), new { id = ticket.Id }, ticket);
        }

        // Ticket torles

        [HttpDelete("DeleteTicket/{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var result = await _ticketService.DeleteTicketAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
