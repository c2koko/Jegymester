using AutoMapper;
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
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            bool isAuthenticated = false;

            // First check if it's a plain text password
            if (user.PasswordHash == userDto.Password)
            {
                isAuthenticated = true;
                // Update to hashed password for future logins
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                try
                {
                    // Only try BCrypt verification if the stored hash looks like a BCrypt hash
                    if (user.PasswordHash.StartsWith("$2"))
                    {
                        isAuthenticated = BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash);
                    }
                }
                catch
                {
                    // If BCrypt verification fails, the hash might be invalid
                    isAuthenticated = false;
                }
            }

            if (!isAuthenticated)
            {
                throw new UnauthorizedAccessException();
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

    }
}
