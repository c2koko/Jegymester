using Jegymester.DataContext.Dtos;
using Jegymester.Dtos;
using Jegymester.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jegymester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateDto movieDto)
        {
            var movie = await _administratorService.CreateMovieAsync(movieDto);
            return CreatedAtAction(nameof(CreateMovie), new { id = movie.Id }, movie);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScreeningById(int id)
        {
            var screening = await _administratorService.GetScreeningByIdAsync(id);
            return Ok(screening);
        }
        [HttpPost]
        public async Task<IActionResult> CreateScreening([FromBody] ScreeningCreateDto screeningDto)
        {
            var screening = await _administratorService.CreateScreeningAsync(screeningDto);
            return CreatedAtAction(nameof(GetScreeningById), new { id = screening.Id }, screening);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _administratorService.DeleteMovieAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScreening(int id, [FromBody] ScreeningUpdateDto screeningDto)
        {
            var screening = await _administratorService.UpdateScreeningAsync(id, screeningDto);
            return Ok(screening);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDto movieDto)
        {
            var movie = await _administratorService.UpdateMovieAsync(id, movieDto);
            return Ok(movie);
        }
    }
}
