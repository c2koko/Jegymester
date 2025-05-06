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
        Task<bool> DeleteTicketAsync(int id);
        Task<TicketDto> GetTicketByIdAsync(int id);
        Task<List<Ticket>> GetTicketByUserIdAsync(int uId);
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

            //specifikáció: 4 órával a vetítés előtt már nem törölhető a jegy, de addig igen
            /*
             * ötlet:
             * 0) új változó: 4 órát levonunk a screeningstarttimeból
             * 1) megnézzük hogy a datetime.now nagyobb e mint az új változó
             * 2) ha igen, akkor exceptiont dobunk
             */

            // 0)
            TimeSpan converted_time = TimeSpan.FromHours(4);
            DateTime deadline = ticket.Screening.ScreeningStartTime - converted_time;
            // 1)
            if (DateTime.Now > deadline) 
            {
                //2)
                throw DeadlineException("Ticket cannot be deleted because there is less than 4 hours until the screening");
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
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

        private Exception DeadlineException(string v)
        {
            throw new NotImplementedException();
        }
    }    
}
/* ============================================= UNDER DEV ========================================= */
