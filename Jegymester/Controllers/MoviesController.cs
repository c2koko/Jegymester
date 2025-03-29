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


        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateDto movieDto)
        {
            var movie = await _movieService.CreateMovieAsync(movieDto);
            return CreatedAtAction(nameof(CreateMovie),new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDto movieDto)
        {
            var movie = await _movieService.UpdateMovieAsync(id, movieDto);
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _movieService.DeleteMovieAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

    }
}
