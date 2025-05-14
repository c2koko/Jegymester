using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Dtos;
using Jegymester.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.Services
{
    public interface IAdministratorService
    {
        //Movie Tasks
        Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto);
        Task<MovieDto> UpdateMovieAsync(int id, MovieUpdateDto moviedto);
        Task<string> DeleteMovieAsync(int movieId);


        //Screening Tasks
        Task<ScreeningDto> CreateScreeningAsync(ScreeningCreateDto screeningCreateDto);
        Task<ScreeningDto> UpdateScreeningAsync(int screeningId, ScreeningUpdateDto screeningUpdateDto);
        Task<bool> DeleteScreeningAsync(int movieId);

    }

    /* ============================================= UNDER DEV ========================================= */
    public class AdministratorService : IAdministratorService
    {
        private readonly JegymesterDbContext _context;
        private readonly IMapper _mapper;

        public AdministratorService(JegymesterDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //------------------------------------Movie Tasks------------------------------------//
        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> UpdateMovieAsync(int id, MovieUpdateDto moviedto)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found!");
            }

            _mapper.Map(moviedto, movie);
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<string> DeleteMovieAsync(int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found!");
            }

            //specifikacio szerint akkor nem lehet torolni ha eppen megy a film vetitese

            /*
             * az ötlet: 
             * 0) kiszedjük a movieból a hosszát és konvertáljuk idővé
             * 1) végigmegyünk minden screeningen
             * 2) megnézzük hogy az keresett film van e benne és ha igen, kiszámoljuk a vetítés végét
             * 3) Megnézzük hogy a datetime.now beleesit e a vetítés kezdete és a vetítés kezdete+ konvertált idő közé (if > && <)
             *      - ha igen, akkor throw OngoingMovieException: (éppen fut a film)
             *      - ha nem, akkor semmi nem történik
             * 4) ha nem, akkor semmi nem történik
             */


            // 0)
            int length = movie.MovieDuration;
            TimeSpan converted_length = TimeSpan.FromMinutes(length);


            // 1)
            foreach (Screening s in _context.Screenings) 
            {
                // 2)
                if (s.Movie.MovieName == movie.MovieName) 
                {
                    DateTime ScreeningEnd = s.ScreeningStartTime + converted_length;

                    //3)
                    if (DateTime.Now > s.ScreeningStartTime && DateTime.Now < ScreeningEnd) 
                    {
                        //4)
                        return "Movie cannot be deleted because there is an ongoing screening of it";
                    }
                }
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return "Movie successfully deleted";
        }

        /*
        public class OngoingMovieException : Exception
        {
            public OngoingMovieException(string message)
                : base(message)
            {
            }
        }*/


        //------------------------------------Screening Tasks------------------------------------//

        public async Task<ScreeningDto> CreateScreeningAsync(ScreeningCreateDto screeningCreateDto)
        {
            var screening = _mapper.Map<Screening>(screeningCreateDto);
            await _context.Screenings.AddAsync(screening);
            await _context.SaveChangesAsync();
            return _mapper.Map<ScreeningDto>(screening);
        }

        
        public async Task<ScreeningDto> UpdateScreeningAsync(int screeningId, ScreeningUpdateDto screeningUpdateDto)
        {
            var screening = await _context.Screenings.FindAsync(screeningId);

            if (screening == null)
            {
                throw new KeyNotFoundException("Screening not found.");
            }

            _mapper.Map(screeningUpdateDto, screening);
            _context.Screenings.Update(screening);
            await _context.SaveChangesAsync();

            return _mapper.Map<ScreeningDto>(screening);
        }

        public async Task<bool> DeleteScreeningAsync(int screeningId)
        {
            var screening = await _context.Screenings.FindAsync(screeningId);
            if (screening == null)
            {
                throw new KeyNotFoundException("Screening does not exist");
            }

            _context.Screenings.Remove(screening);
            await _context.SaveChangesAsync();
            return true;
        }

        /*============================================= UNDER DEV ========================================= */
    }
}