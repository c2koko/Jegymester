/*============================================= UNDER DEV =========================================*/
using Jegymester.Dtos;
using Jegymester.DataContext.Entities;
using Jegymester.DataContext.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Jegymester.DataContext.Dtos;

namespace Jegymester.Services
{
    public interface IScreeningService
    {

        Task<IEnumerable<ScreeningDto>> GetAllScreeningsAsync();
        Task<IEnumerable<ScreeningDto>> GetScreeningsByMovieIdAsync(int movieId);
        Task<ScreeningDto> GetScreeningByIdAsync(int screeningId);
        Task<ScreeningDto> CreateScreeningAsync(ScreeningCreateDto screeningCreateDto); // pipa
         // pipa
         /*
        Task<bool> DeleteScreeningAsync(int screeningId); // pipa
        Task<ScreeningDto> UpdateScreeningAsync(int screeningId, ScreeningUpdateDto screeningUpdateDto); // pipa
        Task<IEnumerable<TicketDto>> GetTicketsToScreeningAsync(int screeningId); // pipa
        Task<IEnumerable<ScreeningDetailsDto>> GetScreeningsByDateAsync(DateTime date); // pipa
        Task<IEnumerable<ScreeningDetailsDto>> GetScreeningsByRoomIdAsync(int roomId);
        */
    }

    public class ScreeningService : IScreeningService
    {
        private readonly JegymesterDbContext _context;
        private readonly IMapper _mapper;

        public ScreeningService(JegymesterDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ScreeningDto> GetScreeningByIdAsync(int screeningId)
        {
            var screening = await _context.Screenings 
                .Include(m => m.Movie) //ez egy loopot okoz
                .Include(t => t.Tickets)
                .FirstOrDefaultAsync(m => m.Id == screeningId);
            //.FindAsync(screeningId);
            if (screening == null)
            {
                throw new KeyNotFoundException("Screening is not found");
            }
            return _mapper.Map<ScreeningDto>(screening);
        }

        public async Task<IEnumerable<ScreeningDto>> GetAllScreeningsAsync()
        {
            var screenings = await _context.Screenings
                .Include(t => t.Tickets)
                .Include(m => m.Movie)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ScreeningDto>>(screenings);
        }

        public async Task<IEnumerable<ScreeningDto>> GetScreeningsByMovieIdAsync(int movieId)
        {
            var screenings = await _context.Screenings
                .Where(m => m.MovieId == movieId)
                .Include(m => m.Movie)
                .ToListAsync();

            if (screenings == null)
            {
                throw new KeyNotFoundException("There's no screening for this movie!");
            }

            return _mapper.Map<IEnumerable<ScreeningDto>>(screenings);
        }

        public async Task<ScreeningDto> CreateScreeningAsync(ScreeningCreateDto screeningCreateDto)
        {

            Screening screening = _mapper.Map<Screening>(screeningCreateDto);

            _context.Screenings.Add(screening);
            await _context.SaveChangesAsync();

            Console.WriteLine(screening.Id);


            List<Chair> chairs = new List<Chair>();

            for (int rows = 0; rows < 10; rows++)
            {
                for (int cols = 0; cols < 10; cols++)
                {
                    chairs.Add(new Chair()
                    {
                        ScreeningId = screening.Id,
                        screening = screening,
                        IsReserved = false,
                    });
                }
            }


            screening.Chairs = chairs;

            _context.Chairs.AddRange(chairs);
            await _context.SaveChangesAsync();

            return _mapper.Map<ScreeningDto>(screening);
        }

        /*
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

        


        /*
        public async Task<IEnumerable<ScreeningDetailsDto>> GetScreeningsByDateAsync(DateTime date)
        {
            var screenings = await dbContext.Screenings
                .Where(s=>s.ScreeningTime == date)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ScreeningDetailsDto>>(screenings);
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
        */
    }
}
/*============================================= UNDER DEV ========================================= */