using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Entities;
using Jegymester.Dtos;
using System;

namespace Jegymester.Services
{
    public interface ITicketService
    {
        Task<TicketDto> CreateTicketAsync(TicketCreateDto ticketDto);
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
        public async Task<TicketDto> CreateTicketAsync(TicketCreateDto foodDto)
        {
            var food = _mapper.Map<Ticket>(foodDto);
            await _context.Tickets.AddAsync(food);
            await _context.SaveChangesAsync();
            return _mapper.Map<TicketDto>(food);
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
