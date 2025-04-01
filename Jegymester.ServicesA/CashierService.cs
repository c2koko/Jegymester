using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Dtos;
using Jegymester.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.Services
{
    public interface ICashierService
    {
        Task<TicketDto> VerifyTicketAsync(int id, TicketVerifyDto ticketdto);
    }

    public class CashierService : ICashierService
    {
        private readonly JegymesterDbContext _context;
        private readonly IMapper _mapper;

        public CashierService(JegymesterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TicketDto> VerifyTicketAsync(int id, TicketVerifyDto ticketdto)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new KeyNotFoundException("Ticket not found!");
            }

            _mapper.Map(ticketdto, ticket);
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();

            return _mapper.Map<TicketDto>(ticket);
        }
    }
}
