using Jegymester.Dtos;
using Jegymester.DataContext.Entities;
using Jegymester.DataContext.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Jegymester.Services
{
    public interface IScreeningService
    {
        Task<ScreeningDto> CreateScreeningAsync(ScreeningCreateDto screeningCreateDto);
        Task<IEnumerable<ScreeningDto>> GetAllScreeningsAsync();
        Task<IEnumerable<ScreeningDto>> GetScreeningsByMovieIdAsync(int movieId);
        Task<ScreeningDto> GetScreeningByIdAsync(int screeningId);
        Task<bool> DeleteScreeningAsync(int screeningId);
        Task<ScreeningDto> UpdateScreeningAsync(int screeningId, ScreeningUpdateDto screeningUpdateDto);
    }

    public class ScreeningService : IScreeningService
    {
        private readonly JegymesterDbContext dbContext;
        private readonly IMapper _mapper;

        public ScreeningService(JegymesterDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<ScreeningDto> CreateScreeningAsync(ScreeningCreateDto screeningCreateDto)
        {
            var screening = new Screening
            {
                MovieId = screeningCreateDto.MovieId,
                RoomId = screeningCreateDto.RoomId,
                ScreeningstartTime = screeningCreateDto.ScreeningTime,
                ScreeningTime = screeningCreateDto.ScreeningTime,
                Tickets = new List<Ticket>(),

            };
            await dbContext.Screenings.AddAsync(screening);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<ScreeningDto>(screening);
        }

        public async Task<bool> DeleteScreeningAsync(int screeningId)
        {
            var screening = await dbContext.Screenings.FindAsync(screeningId);

            if(screening == null)
            {
                throw new KeyNotFoundException("Screening not found.");
            }

            dbContext.Screenings.Remove(screening);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ScreeningDto>> GetAllScreeningsAsync()
        {
            var screenings = await dbContext.Screenings
                .Include(t=>t.Tickets)
                .Include(r=>r.Room)
                .Include(m=>m.Movie)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ScreeningDto>>(screenings);
        }

        public async Task<ScreeningDto> GetScreeningByIdAsync(int screeningId)
        {
            var screening = await dbContext.Screenings.FindAsync(screeningId);
            if(screening == null)
            {
                throw new KeyNotFoundException("Screening is not found");
            }
            return _mapper.Map<ScreeningDto>(screening);
        }

        public Task<IEnumerable<ScreeningDto>>? GetScreeningsByMovieIdAsync(int movieId)
        {
            var screenings = dbContext.Screenings
                .Where(m => m.MovieId == movieId)
                .ToListAsync();
            return null;
            //return _mapper.Map<IEnumerable<ScreeningDto>>(screenings);
            //Ez még nem jó, úgyhogy erre rá kell néznem majd (By: Henrik)
        }

        public async Task<ScreeningDto> UpdateScreeningAsync(int screeningId, ScreeningUpdateDto screeningUpdateDto)
        {
            var screening = await dbContext.Screenings.FindAsync(screeningId);

            if(screening == null)
            {
                throw new KeyNotFoundException("Screening not found.");
            }
            dbContext.Screenings.Update(screening);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<ScreeningDto>(screening);

            

           
        }
    }
}
