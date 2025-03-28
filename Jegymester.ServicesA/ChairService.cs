using Jegymester.DataContext.Data;
using Jegymester.DataContext.Entities;
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
    }
    class ChairService : IChairService
    {
        JegymesterDbContext _dbContext;
        public ChairService(JegymesterDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<IEnumerable<Chair>> GetAllChair()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateReservation(int id)
        {
            var chair = _dbContext.Chairs.Where(i => i.Id == id); // id kiválasztva
            throw new NotImplementedException();
        }
    }
}
