/* ============================================= UNDER DEV ========================================= */
using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Entities;
using Jegymester.Dtos;
using System;

namespace Jegymester.Services
{
    public interface ITicketService
    {
        Task<TicketDto> CreateTicketAsync(TicketCreateDto ticketDto, int? userId);
        Task<bool> DeleteTicketAsync(int id);
    }
    public class TicketService : ITicketService
    {
        private readonly JegymesterDbContext _context;
        private readonly IMapper _mapper;

        public TicketService(JegymesterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //create ticket
        public async Task<TicketDto> CreateTicketAsync(TicketCreateDto ticketDto, int? userId)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            ticket.UserId = userId; // NULL ha a user nem regisztrált

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return _mapper.Map<TicketDto>(ticket);
        }

        //delete ticket
        public async Task<bool> DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new KeyNotFoundException("Ticket not found.");
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
/* ============================================= UNDER DEV ========================================= */
