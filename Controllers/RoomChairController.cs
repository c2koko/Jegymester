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
    public class RoomChairController : ControllerBase
    {
        JegymesterDbContext _dbContext;
        public RoomChairController(JegymesterDbContext dbContext)
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

