using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Dtos;
using Jegymester.DataContext.Entities;
using Jegymester.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.Services
{
    public interface INotRegisteredUserervice
    {
        Task<NotRegisteredUserDto> CreateNotRegisteredUserAsync(NotRegisteredUserDto userDto, string email, string phone);
        Task<TicketDto> CreateTicketForNotRegisteredUserAsync(TicketCreateDto ticketDto, string email, string phone);
    }
    public class NotRegisteredUserService : INotRegisteredUserervice
    {
        private readonly JegymesterDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public NotRegisteredUserService(JegymesterDbContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _configuration = config;
        }

        public async Task<NotRegisteredUserDto> CreateNotRegisteredUserAsync(NotRegisteredUserDto userDto, string email, string phone) 
        {
            bool AlreadyExists = false;

            foreach (User u in _context.Users)
            {
                if (u.Name == "placeholder" && u.Email == email && u.Phone == phone) 
                {
                    AlreadyExists = true;
                }
            }

            var nruser = _mapper.Map<User>(userDto);

            if (AlreadyExists == false)
            {   
                nruser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                nruser.Email = email;
                nruser.Phone = phone;

                await _context.Users.AddAsync(nruser);
                await _context.SaveChangesAsync();      
            }

            return _mapper.Map<NotRegisteredUserDto>(nruser);
        }

        public async Task<TicketDto> CreateTicketForNotRegisteredUserAsync(TicketCreateDto ticketDto, string email, string phone) 
        {
            int id = 0;

            foreach (User u in _context.Users)
            {
                if (u.Name == "placeholder" && u.Email == email && u.Phone == phone)
                {
                    id = u.Id;
                }
            }

            var ticket = _mapper.Map<Ticket>(ticketDto);
            ticket.UserId = id;

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return _mapper.Map<TicketDto>(ticket);
        }
    }
}
