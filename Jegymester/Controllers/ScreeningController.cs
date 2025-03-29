using Jegymester.Dtos;
using Jegymester.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jegymester.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningController : ControllerBase
    {
        public readonly IScreeningService _screeningService;

        public ScreeningController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllScreenings()
        {
            var screenings = await _screeningService.GetAllScreeningsAsync();
            return Ok(screenings);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetScreeningById(int id)
        {
            var screening = await _screeningService.GetScreeningByIdAsync(id);
            return Ok(screening);
        }


        [HttpPost]
        public async Task<IActionResult> CreateScreening([FromBody] ScreeningCreateDto screeningDto)
        {
            var screening = await _screeningService.CreateScreeningAsync(screeningDto);
            return CreatedAtAction(nameof(GetScreeningById), new { id = screening.Id }, screening);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScreening(int id, [FromBody] ScreeningUpdateDto screeningDto)
        {
            var screening = await _screeningService.UpdateScreeningAsync(id, screeningDto);
            return Ok(screening);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreening(int id)
        {
            var result = await _screeningService.DeleteScreeningAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }


        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetScreeningsByMovieId(int movieId)
        {
            var screenings = await _screeningService.GetScreeningsByMovieIdAsync(movieId);
            return Ok(screenings);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketsToScreening(int id)
        {
            var tickets = await _screeningService.GetTicketsToScreeningAsync(id);
            return Ok(tickets);
        }


        [HttpGet("{date}")]
        public async Task<IActionResult> GetScreeningsByDate(DateTime date)
        {
            var screenings = await _screeningService.GetScreeningsByDateAsync(date);
            return Ok(screenings);
        }


        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetScreeningsByRoomId(int roomId)
        {
            var screenings = await _screeningService.GetScreeningsByRoomIdAsync(roomId);
            return Ok(screenings);
        }
    }
}
