using Jegymester.DataContext.Entities;
using System.ComponentModel.DataAnnotations;

namespace Jegymester.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public User? User { get; set; } //teszt idejéig nullable
        public Screening? Screening { get; set; } //teszt idejéig nullable
    }

    public class TicketCreateDto
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Price { get; set; }
        public int ScreeningId { get; set; }
    }

}
