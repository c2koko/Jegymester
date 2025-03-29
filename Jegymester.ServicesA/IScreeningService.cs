using Jegymester.Dtos;
using Jegymester.DataContext.Entities;
using Jegymester.DataContext.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Jegymester.Services
{
    public interface IScreeningService
    {
        Task<ScreeningDto> CreateScreeningAsync(ScreeningCreateDto screeningCreateDto); // pipa
        Task<IEnumerable<ScreeningDto>> GetAllScreeningsAsync(); // pipa
        Task<IEnumerable<ScreeningDto>> GetScreeningsByMovieIdAsync(int movieId); // pipa
        Task<ScreeningDto> GetScreeningByIdAsync(int screeningId); // pipa
        Task<bool> DeleteScreeningAsync(int screeningId); // pipa
        Task<ScreeningDto> UpdateScreeningAsync(int screeningId, ScreeningUpdateDto screeningUpdateDto); // pipa
        Task<IEnumerable<TicketDto>> GetTicketsToScreeningAsync(int screeningId); // pipa
        Task<IEnumerable<ScreeningDetailsDto>> GetScreeningsByDateAsync(DateTime date); // pipa
        Task<IEnumerable<ScreeningDetailsDto>> GetScreeningsByRoomIdAsync(int roomId);
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

        public async Task<IEnumerable<ScreeningDetailsDto>> GetScreeningsByDateAsync(DateTime date)
        {
            var screenings = await dbContext.Screenings
                .Where(s=>s.ScreeningTime == date)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ScreeningDetailsDto>>(screenings);
        }

       

        public async Task<IEnumerable<ScreeningDto>> GetScreeningsByMovieIdAsync(int movieId)
        {
            var screenings = dbContext.Screenings
                .Where(m => m.MovieId == movieId)
                .Include(m=>m.Movie)
                .ToListAsync();

            if (screenings == null)
            {
                throw new KeyNotFoundException("There's no screening for this movie!");
            }

            return _mapper.Map<IEnumerable<ScreeningDto>>(screenings);
        }

        public async Task<IEnumerable<ScreeningDetailsDto>> GetScreeningsByRoomIdAsync(int roomId)
        {
            var screenings = dbContext.Screenings
                .Where(r => r.RoomId == roomId)
                .Include(r => r.Room)
                .ToListAsync();

            if (screenings == null)
            {
                throw new KeyNotFoundException("There's no screening in this room!");
            }
            return _mapper.Map<IEnumerable<ScreeningDetailsDto>>(screenings);
           
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsToScreeningAsync(int screeningId)
        {
            var screening = await dbContext.Screenings
                 .Where(s => s.Id == screeningId)
                 .Include(t => t.Tickets)
                 .FirstOrDefaultAsync();

            if (screening == null)
            {
                throw new KeyNotFoundException("Screening is not found!");
            }
            return _mapper.Map<IEnumerable<TicketDto>>(screening.Tickets);


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
