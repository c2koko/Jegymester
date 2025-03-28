using Jegymester.DataContext.Data;
using Jegymester.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.Services
{
    public interface IChairService
    {
        Task<IEnumerable<Chair>> GetAllChair(); // visszaadja a székeket kihangsúlyozva foglalt-e
        Task<bool> UpdateReservation(int id); // jegyvásárlás és film lejátszás után átírja a bool-t
        Task<IEnumerable<Chair>> GetAvailableChairsForRoom(int room);// front-end interface - szabad székek kilistázása miatt kelleni fog, fontos
    }
    class ChairService : IChairService
    {
        private readonly JegymesterDbContext _dbContext;
        public ChairService(JegymesterDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Chair>> GetAllChair()
        {
            return await _dbContext.Chairs.ToListAsync();
        }

        public async Task<IEnumerable<Chair>> GetAvailableChairsForRoom(int room)
        {
            return await _dbContext.Chairs.Where(c => c.RoomId == room && !c.IsReserved).ToListAsync();
        }

        public async Task<bool> UpdateReservation(int id)
        {
            var chair = await _dbContext.Chairs.Where(i => i.Id == id).FirstOrDefaultAsync(); // id kiválasztva
            if(chair == null)
            {
                return false;
            }
            chair.IsReserved = !chair.IsReserved;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
