/* ============================================= UNDER DEV ========================================= */
using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Entities;
using Jegymester.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Sockets;

namespace Jegymester.Services
{
    public interface ITicketService
    {
        Task<TicketDto> CreateTicketAsync(TicketCreateDto ticketDto, int? userId);
        Task<string> DeleteTicketAsync(int id);
        Task<TicketDto> GetTicketByIdAsync(int id);
        Task<List<Ticket>> GetTicketByUserIdAsync(int uId);
        Task<List<TicketDto>> GetTicketsByScreening(int screeningId);
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
            //ticket.UserId = userId; // NULL ha a user nem regisztrált

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return _mapper.Map<TicketDto>(ticket);
        }

        //delete ticket
        public async Task<string> DeleteTicketAsync(int id)
        {
            try
            {
                // így már betölti a kapcsolódó Screening-et is
                var ticket = await _context.Tickets
                    .Include(t => t.Screening)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (ticket == null)
                    throw new KeyNotFoundException("Ticket not found.");

                // 4 órával a vetítés előtt nem törölhető
                var deadline = ticket.Screening.ScreeningStartTime.AddHours(-4);
                if (DateTime.Now > deadline)
                    return "Ticket cannot be deleted because there is less than 4 hours until the screening";

                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
                return "Ticket successfully deleted";
            }
            catch (Exception e)
            {
                // itt lehet konkrétabban is kezelni a DeadLineException-t vagy KeyNotFound-t
                throw;
            }
        }

        public async Task<TicketDto> GetTicketByIdAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new KeyNotFoundException("Ticket is not found!"); 
            }
            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task<List<Ticket>> GetTicketByUserIdAsync(int uId)
        {
            //nem tudom miért, de az includeos verzio nagyon nem azt adja vissza amit kéne

            //var lista = await _context.Tickets
            //.Where(t => t.UserId == uId)
            //.Include(ticket => ticket.Screening)
            //   .ThenInclude(s => s.Movie)
            //.Include(t => t.User)
            //.ToListAsync();

            var lista = await _context.Tickets
            .Where(t => t.UserId == uId)
            .ToListAsync();


            return lista;
        }

        public async Task<List<TicketDto>> GetTicketsByScreening(int screeningId)
        {
            var tickets = await _context.Tickets.Where(t => t.ScreeningId == screeningId && !t.TicketVerified).ToListAsync();

            if (tickets == null || tickets.Count == 0)
            { 
                return null;
            }

            /*
             List<TicketDto> dtos = new List<TicketDto>();

            foreach (Ticket t in tickets)
            {
                dtos.Add(_mapper.Map<TicketDto>(t));
            }
             */

            return _mapper.Map<List<TicketDto>>(tickets);
        }

        
        public class DeadLineException : Exception
        {
            public DeadLineException(string message)
                : base(message)
            {
            }
        }
        
    }    
}
/* ============================================= UNDER DEV ========================================= */
