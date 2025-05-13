using Jegymester.DataContext.Data;
using Jegymester.DataContext.Dtos;
using Jegymester.DataContext.Entities;
using Jegymester.Services;
using Microsoft.AspNetCore.Mvc;


namespace Jegymester
{
    [ApiController]
    [Route("api/chairs")]
    public class ChairController : ControllerBase
    {
        JegymesterDbContext _dbContext;
        private readonly IChairService _chairService;
        public ChairController(JegymesterDbContext dbContext, IChairService chairService)
        {
            _dbContext = dbContext;
            _chairService = chairService;
        }
        [HttpPatch("UpdateReservation/{id}")] // részleges módosítás
        public async Task<IActionResult> UpdateReservation(int id)
        {
            try
            {
                var toggle = await _chairService.UpdateReservation(id);
                if (!toggle)
                {
                    return NotFound("A szék nem található, a státusza így nem lett módosítva");
                }
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChairDto>>> GetAllChairs()
        {
            try
            {
                return Ok(await _chairService.GetAllChair());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("GetAvailableChairsForRoom/{screening}")]
        public async Task<IActionResult> GetAvailableChairsForRoom(int screening)
        {
            var availableChairs = await _chairService.GetAvailableChairsForRoom(screening);
            return Ok(availableChairs);
        }
    }
}



/*
{
    [Route("api/Movie")]
    [ApiController]
    public class ChairController : ControllerBase
    {
        JegymesterDbContext _dbContext;
        public ChairController(JegymesterDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // HttpPut egész objektumot ad vissza // ha csak egy részét, akkor HttpPatch
        // https://medium.com/@niteshsinghal85/httpput-or-httppatch-in-asp-net-core-ceaae99e320b
        [HttpGet("RC.Room={RoomId};Rc.Chair={ChairId}")]
        public ActionResult<RoomChair> GetRoomChair(int RoomId, int ChairId)
        {
            var rc = _dbContext.RoomsChairs.ToList();
            if (rc.Any(rc => rc.RoomId == RoomId && rc.ChairId == ChairId))
            {
                return Ok(rc.Where(rc => rc.RoomId == RoomId && rc.ChairId == ChairId));
            }
            return NotFound();
        }
    }
}
// szobában levő székek kezelése
//public List<bool> Chairs = new List<bool>(100); // 10x10 terem

/*public void InitializingChairs()
{
    for (int i = 0; i < 100; i++)
    {
        Chairs.Add(false); // ha üres: 0
    }
}
[MethodImpl(MethodImplOptions.Synchronized)]
public void BookChair(int chairToBeBooked) // a kapott érték eggyel igazított
{
    if(!Chairs.Any(c => c == false))
    {
        Console.WriteLine("a teremben nincsen ütes hely");
    }
    // szabad e a kiválasztott szék
    if (Chairs[chairToBeBooked] == false)
    {
        Chairs[chairToBeBooked] = true;
        Console.WriteLine("sikeres jegyfoglalás"); 
    }
    Console.WriteLine("hiba, a szék nem elérhető(jegyfoglalás a Room.cs-ben)"); 
}
public string GetEmptyChairs()
{
    string emptyChairsString = "Szabad székek: \n";
    for (int i = 0; i < 99; i++)
    {
        if (!Chairs[i])
        {
            int dim1 = 0;
            double dim2 = 0;
            dim1 = (i % 10)+1;
            dim2 = Math.Floor((double)i / 10)+1;
            emptyChairsString += $"{dim2} sor, {dim1} oszlop \n";
        }
    }
    return emptyChairsString;
}*/
