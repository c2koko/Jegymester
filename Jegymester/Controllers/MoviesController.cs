using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jegymester.Entities;
using Microsoft.EntityFrameworkCore;
using Jegymester.Data;


namespace Jegymester
{
    [Route("api/Movie")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        JegymesterDbContext _dbContext;

        public MoviesController(JegymesterDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("M.Id={Id}")]
        public ActionResult<Movies> GetMovie(int Id)
        {
            /*
            if ()
            {
                return Ok(_M.Any(m => m.Id == 1));
            }

            return NotFound();
            */

            return NotFound();
        } 
    }
}
