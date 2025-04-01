using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Dtos;
using Jegymester.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.Services
{
    public interface IUserService 
    {
        Task<UserDto> RegisterUserAsync(UserRegisterDto userDto);
        
        Task<UserDto> UpdateUserAsync(int userId, UserUpdateDto userDto);
        //Task<string> LoginAsync(UserLoginDto userDto);
    }
    public class UserService : IUserService
    {
        private readonly JegymesterDbContext _context;
        private readonly IMapper _mapper;

        public UserService(JegymesterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> RegisterUserAsync(UserRegisterDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
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
    }
}
