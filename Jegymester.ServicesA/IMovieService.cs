using Jegymester.DataContext.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.Services
{
    public interface IMovieService
    {
        Task<MovieDto> GetMovieByIdAsync(int movieId);
        Task<IEnumerable<MovieDto>> GetAllMoviesOnScreenengAsync();
        Task<bool> UpdateMovieAsync(MovieDto dto);
        Task<bool> DeleteMovieAsync(int movieId);
        Task<bool> UpdateMovieAsync(MovieUpdateDto dto);
    }

    public class MovieService : IMovieService
    {
        public async Task<bool> DeleteMovieAsync(int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesOnScreenengAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<MovieDto> GetMovieByIdAsync(int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMovieAsync(MovieDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateMovieAsync(MovieUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
