using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jegymester.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Jegymester.DataContext.Data;
using Jegymester.Services;
using Jegymester.DataContext.Dtos;


namespace Jegymester
{
    [Route("api/Movie")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
       private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("GetMovieById/{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            return Ok(movie);
        }
    }
}