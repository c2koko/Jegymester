using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Dtos;
using Jegymester.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMovies();
        Task<MovieDto> GetMovieByIdAsync(int movieId);
        Task<IEnumerable<MovieDto>> GetAllMoviesOnScreenengAsync();
        Task<bool> DeleteMovieAsync(int movieId);
        Task<MovieDto> UpdateMovieAsync(int id,MovieUpdateDto moviedto);
        Task<MovieDto> CreateMovieAsync(MovieCreateDto movieDto);
    }

    public class MovieService : IMovieService
    {
        private readonly JegymesterDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(JegymesterDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found!");
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MovieDto>> GetAllMovies()
        {
            var movies = await _context.Movies
                .Include(s => s.Screenings)
                .ToListAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }
        //Ebben nem vagyok biztos, hogy azt csinálja, úgyhogy majd meg kell beszélni!
        public async Task<IEnumerable<MovieDto>> GetAllMoviesOnScreenengAsync()
        {
            var movies = await _context.Movies
                .Include(m=>m.Screenings)
                .ToListAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetMovieByIdAsync(int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie==null)
            {
                throw new KeyNotFoundException("Movie not found!");
            }
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> UpdateMovieAsync(int id,MovieUpdateDto moviedto)
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
    }
}
