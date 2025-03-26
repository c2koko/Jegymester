using Microsoft.AspNetCore.Mvc;
using Jegymester.DataContext.Data;
using Microsoft.EntityFrameworkCore;
using Jegymester.DataContext.Entities;
using Jegymester.Services;
using Jegymester.Dtos;

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

        // teszt list
        static private List<Ticket> tickets = new List<Ticket>
        {
            /*
        public int Id
        public DateTime? Date //teszt idejéig nullable
        public int Price
        public int UserId
        public User? User
        public int ScreeningId
        public Screening? Screening
            */
            new Ticket
            {
                Id = 1,
                Date = DateTime.Now,
                Price = 1000,
                UserId = 1,
                User = null,
                ScreeningId = 1,
                Screening = null
            },
            new Ticket
            {
                Id = 2,
                Date = DateTime.Now,
                Price = 1500,
                UserId = 9,
                User = null,
                ScreeningId = 2,
                Screening = null
            },
            new Ticket
            {
                Id = 3,
                Date = DateTime.Now,
                Price = 2000,
                UserId = 12,
                User = null,
                ScreeningId = 2,
                Screening = null
            },
            new Ticket
            {
                Id = 4,
                Date = DateTime.Now,
                Price = 6900,
                UserId = 9,
                User = null,
                ScreeningId = 2,
                Screening = null
            }
        };

        // Ticket letrehozas

        [HttpPost("CreateTicket")]

        /*old
        public ActionResult<Ticket> CreateTicket(Ticket newTicket)
        {
            if (newTicket is null) { return BadRequest(); }

            newTicket.Id = tickets.Max(t => t.Id) + 1; // ez a database nelkuli teszteles alatt kell, kesobb ki kell kommentezni v torolni
            
            tickets.Add(newTicket);
            return CreatedAtAction(nameof(GetTicketById), new { id = newTicket.Id }, newTicket);
        }
        */
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDto ticketDto)
        {
            var ticket = await _ticketService.CreateTicketAsync(ticketDto);
            //return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
            return CreatedAtAction("nev", new { id = ticket.Id }, ticket);
        }

        // Ticket torles

        [HttpDelete("DeleteTicket/{id}")]

        public ActionResult<Ticket> DeleteTicket(int id)
        {
            var ticket = tickets.FirstOrDefault(t => t.Id == id);

            if (ticket is null) { return NotFound(); }

            tickets.Remove(ticket);
            return NoContent();
        }


        //Ticket lekeres id alapjan

        [HttpGet("TicketById/{ticket_id}")]
        public ActionResult<Ticket> GetTicketById(int ticket_id)
        {
            var ticket = tickets.FirstOrDefault(t => t.Id == ticket_id);

            if (ticket is null) { return NotFound(); }
            return Ok(ticket);
        }

        //Ticket lekeres Userid alapjan

        [HttpGet("TicketByUserId/{uid}")]
        public ActionResult<Ticket> GetTicketByUserId(int uid)
        {
            var ticket = tickets.FindAll(t => t.UserId == uid);

            if (ticket is null) { return NotFound(); }
            return Ok(ticket);
        }

        //Osszes ticket lekeres

        [HttpGet("GetAllTickets")]
        public ActionResult<List<Ticket>> GetAllTickets()
        {
            return Ok(tickets);
        }

    }
}