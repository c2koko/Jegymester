using Microsoft.AspNetCore.Mvc;
using Jegymester.DataContext.Data;
using Microsoft.EntityFrameworkCore;
using Jegymester.DataContext.Entities;
using Jegymester.Services;
using Jegymester.Dtos;
using Jegymester.DataContext.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Jegymester.Controllers
{
    [Route("api/Ticket")]
    [ApiController]
    [Authorize(Roles = "RegisteredUser")]
    public class TicketController : ControllerBase
    {

        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }


        [HttpPost("CreateTicket")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateTicketAnonymusAsync([FromBody] TicketCreateDto ticketDto)
        {
            var ticket = await _ticketService.CreateTicketAsync(ticketDto, null);
            return CreatedAtAction(nameof(CreateTicket), new { id = ticket.Id }, ticket);
        }

        [HttpPost("CreateTicketRegisteredUser")]
        [Authorize(Roles = "RegisteredUser")]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDto ticketDto)
        {
            int userIdFromClaim = int.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            var ticket = await _ticketService.CreateTicketAsync(ticketDto, userIdFromClaim);
            return CreatedAtAction(nameof(CreateTicket), new { id = ticket.Id }, ticket);
        }

        // Ticket torles

        [HttpDelete("DeleteTicket/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var result = await _ticketService.DeleteTicketAsync(id);
            /*if (result)
            {
                return Ok(result);
            }*/
            return Ok(result);
        }

        [HttpGet("GetTicketById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            return Ok(ticket);
        }

        [HttpGet("GetTicketsByUserId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTicketsByUserId(int id)
        {
            var tickets = await _ticketService.GetTicketByUserIdAsync(id);
            return Ok(tickets);
        }


        [HttpGet("GetTicketForScreening/{id}")]
        [Authorize(Roles = "Cashier")]
        public async Task<IActionResult> GetTicketsForScreening(int screeningId)
        {
            List<TicketDto> tickets = await _ticketService.GetTicketsByScreening(screeningId);

            if (tickets != null && tickets.Count != 0)
            {
                return Ok(tickets);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
