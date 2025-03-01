using Jegymester.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jegymester.Data
{
    public class JegymesterDbContext(DbContextOptions<JegymesterDbContext> options) : DbContext(options)
    {
        public DbSet<Ticket> Tickets => Set<Ticket>();
    }
}
