﻿using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Dtos;
using Jegymester.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.Services
{
    public interface IUserService 
    {
        Task<UserDto> RegisterUserAsync(UserRegisterDto userDto);
        Task<string> LoginUserAsync(UserLoginDto userDto);
        
        Task<UserDto> UpdateUserAsync(int userId, UserUpdateDto userDto);
        Task<UserInfoDto> GetUserInfoAsync(int userId);
    }
    public class UserService : IUserService
    {
        private readonly JegymesterDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(JegymesterDbContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _configuration = config;
        }

        public async Task<UserDto> RegisterUserAsync(UserRegisterDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            Console.WriteLine($"user email: {user.Email}, passwordHash: {user.PasswordHash}, roleId: {user.RoleId}");


            if (user.RoleId == null)
            {
                Role existingRole = await _context.Roles.FirstOrDefaultAsync(role => role.PermaId == userDto.RoleId);
                if (existingRole != null) user.Role = existingRole;
            }

            if (user.RoleId == null)
            {
                user.Role = await GetDefaultCustomerRoleAsync();
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        private async Task<Role> GetDefaultCustomerRoleAsync()
        {
            var customerRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "RegisteredUser");
            if (customerRole == null)
            {
                customerRole = new Role { RoleName = "RegisteredUser", PermaId = 1 };
                await _context.Roles.AddAsync(customerRole);
                await _context.SaveChangesAsync();
            }
            return customerRole;
        }

        public async Task<string> LoginUserAsync(UserLoginDto userDto)
        {
            var user = await _context.Users.Include(user => user.Role).FirstOrDefaultAsync(user => user.Email == userDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

           

            return await GenerateToken(user);
        }

        public async Task<UserDto> UpdateUserAsync(int userId, UserUpdateDto userDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist");
            }

            _mapper.Map(userDto, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        private async Task<string> GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var id = await GetClaimsIdentity(user);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], id.Claims, expires: expires, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<ClaimsIdentity> GetClaimsIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString(CultureInfo.InvariantCulture))
            };

            if (user.Role != null)
            {
                claims.Add(new Claim("roleId", Convert.ToString(user.Role.Id)));
                claims.Add(new Claim(ClaimTypes.Role, user.Role.RoleName));
            }

            return new ClaimsIdentity(claims, "Token");
        }


        public async Task<UserInfoDto> GetUserInfoAsync(int userId)
        {
            // Betöltjük a felhasználót az adatbázisból
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                // visszaadjuk a felhasználó nevét
                return new UserInfoDto {
                    Name = user.Name,
                    Email = user.Email,
                };
            }
            else
            {
                // Ha a felhasználó nem található, akkor null-t adunk vissza és lekezeljük a kontrollerben
                return null;
            }
        }
    }
}
