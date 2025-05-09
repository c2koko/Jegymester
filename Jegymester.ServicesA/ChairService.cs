using AutoMapper;
using Jegymester.DataContext.Data;
using Jegymester.DataContext.Dtos;
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
        Task<IEnumerable<ChairDto>> GetAllChair(); // visszaadja a székeket kihangsúlyozva foglalt-e
        Task<bool> UpdateReservation(int id); // jegyvásárlás és film lejátszás után átírja a bool-t
        Task<IEnumerable<ChairDto>> GetAvailableChairsForRoom(int screening);// front-end interface - szabad székek kilistázása miatt kelleni fog, fontos
    }
    public class ChairService : IChairService
    {
        private readonly JegymesterDbContext _dbContext;
        private readonly IMapper _mapper;
        public ChairService(JegymesterDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ChairDto>> GetAllChair()
        {
            return (IEnumerable<ChairDto>)_mapper.Map<List<Chair>, List<ChairDto>>(await _dbContext.Chairs.ToListAsync());

            /* In cas mapping would not work
            List<Chair> chiars = await _dbContext.Chairs.ToListAsync();
            List<ChairDto> mappedChirs = new List<ChairDto>();
            chiars.ForEach(chair =>
            {
                mappedChirs.Add(_mapper.Map<ChairDto>(chair));
            });

            return mappedChirs;
            */
        }

        public async Task<IEnumerable<ChairDto>> GetAvailableChairsForRoom(int screening)
        {
            return (IEnumerable<ChairDto>)_mapper.Map<List<Chair>, List<ChairDto>>(await _dbContext.Chairs.Where(c => c.ScreeningId == screening && !c.IsReserved).ToListAsync());
        }

        public async Task<bool> UpdateReservation(int id)
        {
            var chair = await _dbContext.Chairs.FindAsync(id);
            if (chair == null)
            {
                return false;
            }
            chair.IsReserved = !chair.IsReserved;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
