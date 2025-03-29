using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Dtos;
using Jegymester.DataContext.Entities;
using Jegymester.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.Services
{
    public interface IAdministratorService
    {
        Task<ScreeningDto> CreateScreeningAsync(ScreeningCreateDto screeningCreateDto);
        Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto);
        Task<bool> DeleteMovieAsync(int movieId);
        Task<MovieDto> UpdateMovieAsync(int id, MovieUpdateDto moviedto);
        Task<ScreeningDto> GetScreeningByIdAsync(int screeningId);
        Task<ScreeningDto> UpdateScreeningAsync(int screeningId, ScreeningUpdateDto screeningUpdateDto);
    }
    public class AdministratorService : IAdministratorService
    {
        private readonly JegymesterDbContext _context;
        private readonly IMapper _mapper;

        public AdministratorService(JegymesterDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ScreeningDto> CreateScreeningAsync(ScreeningCreateDto screeningCreateDto)
        {
            var screening = _mapper.Map<Screening>(screeningCreateDto);
            await _context.Screenings.AddAsync(screening);
            await _context.SaveChangesAsync();

            return _mapper.Map<ScreeningDto>(screening);
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> DeleteMovieAsync(int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            var screeningCount = await _context.Movies
                .SelectMany(s => s.Screenings)
                .CountAsync(m => m.MovieId == movieId);

            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found!");
            }
            if(screeningCount != 0)
            {
                throw new Exception("Movie cannot be deleted because there screenings with it!");
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ScreeningDto> UpdateScreeningAsync(int screeningId, ScreeningUpdateDto screeningUpdateDto)
        {
            var screening = await _context.Screenings.FindAsync(screeningId);

            if (screening == null)
            {
                throw new KeyNotFoundException("Screening not found.");
            }
            _context.Screenings.Update(screening);
            await _context.SaveChangesAsync();

            return _mapper.Map<ScreeningDto>(screening);
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

        public async Task<ScreeningDto> GetScreeningByIdAsync(int screeningId)
        {
            var screening = await _context.Screenings.FindAsync(screeningId);
            if (screening == null)
            {
                throw new KeyNotFoundException("Screening is not found");
            }
            return _mapper.Map<ScreeningDto>(screening);
        }
    }
}
