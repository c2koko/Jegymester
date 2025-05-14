/* ============================================= UNDER DEV =========================================*/
using Jegymester.DataContext.Dtos;
using Jegymester.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jegymester.Controllers
{
    [Route("api/Admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        //------------------------------------Movie Tasks------------------------------------//


        [HttpPost("CreateMovie")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateDto movieDto)
        {
            var movie = await _administratorService.CreateMovieAsync(movieDto);
            return CreatedAtAction(nameof(CreateMovie), new { id = movie.Id }, movie);
        }



        [HttpPut("UpdateMovie/{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDto movieDto)
        {
            var movie = await _administratorService.UpdateMovieAsync(id, movieDto);
            return Ok(movie);
        }



        [HttpDelete("DeleteMovie/{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _administratorService.DeleteMovieAsync(id);
            /*if (result)
            {
                return NoContent();
            }*/
            return NotFound();
        }

        //------------------------------------Screening Tasks------------------------------------//
        /*
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScreeningById(int id)
        {
            var screening = await _administratorService.GetScreeningByIdAsync(id);
            return Ok(screening);
        }
        */
        [HttpPost("CreateScreening")]
        public async Task<IActionResult> CreateScreening([FromBody] ScreeningCreateDto screeningDto)
        {
            var screening = await _administratorService.CreateScreeningAsync(screeningDto);
            return CreatedAtAction(nameof(CreateScreening), new { id = screening.Id }, screening);
        }


   
        [HttpPut("UpdateScreening/{id}")]
        public async Task<IActionResult> UpdateScreening(int id, [FromBody] ScreeningUpdateDto screeningDto)
        {
            var screening = await _administratorService.UpdateScreeningAsync(id, screeningDto);
            return Ok(screening);
        }



        [HttpDelete("DeleteScreening/{id}")]
        public async Task<IActionResult> DeleteScreening(int id)
        {
            var result = await _administratorService.DeleteScreeningAsync(id);
            /*if (result)
            {
                Ok(result);
            }*/
            return Ok(result);
        }




        /*============================================= UNDER DEV ========================================= */
    }
}
