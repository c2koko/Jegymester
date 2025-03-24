using Jegymester.Data;
using Jegymester.Dtos;
using Jegymester.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace Jegymester.Services
{
    public interface IScreeningService
    {
        Task<ScreeningDto> CreateScreeningAsync(ScreeningCreateDto screeningCreateDto);
        Task<IEnumerable<ScreeningDto>> GetAllScreeningsAsync();
        Task<IEnumerable<ScreeningDto>> GetScreeningsByMovieIdAsync(int movieId);
        Task<ScreeningDto> GetScreeningByIdAsync(int screeningId);
        Task<bool> DeleteScreeningAsync(int screeningId);
        Task UpdateScreeningAsync(int screeningId, ScreeningUpdateDto screeningUpdateDto);
    }

    public class ScreeningService : IScreeningService
    {
        private readonly JegymesterDbContext dbContext;

        public ScreeningService(JegymesterDbContext dbContext)
        {
            this.dbContext = dbContext;
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
            //return _mapper.Map<IEnumerable<ScreeningDto>>(screenings);
        }

        public async Task<ScreeningDto> GetScreeningByIdAsync(int screeningId)
        {
            var screening = await dbContext.Screenings.FindAsync(screeningId);
            if(screening == null)
            {
                throw new KeyNotFoundException("Screening is not found");
            }
            //return _mapper.Map<ScreeningDto>(screening);
        }

        public Task<IEnumerable<ScreeningDto>> GetScreeningsByMovieIdAsync(int movieId)
        {
            var screenings = dbContext.Screenings
                .Where(m => m.MovieId == movieId)
                .ToListAsync();

            //return _mapper.Map<IEnumerable<ScreeningDto>>(screenings);
        }

        public async Task UpdateScreeningAsync(int screeningId, ScreeningUpdateDto screeningUpdateDto)
        {
            var screening = await dbContext.Screenings.FindAsync(screeningId);

            if(screening == null)
            {
                throw new KeyNotFoundException("Screening not found.");
            }

            //Ez fogja megcsinálni a hozzárendelést!
            //_mapper.Map(screeningUpdateDto, screening);

            

            await dbContext.SaveChangesAsync();
        }
    }
}
