
using Jegymester.DataContext.Entities;
using System.ComponentModel.DataAnnotations;

namespace Jegymester.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int Price { get; set; }
        public bool TicketVerified { get; set; } = false; 
        public int UserId { get; set; } 
        public User User { get; set; }
        public int ScreeningId { get; set; } 
        public Screening Screening { get; set; }
    }

    public class TicketCreateDto
    {
        public DateTime DateOfPurchase { get; set; }
        public int Price { get; set; }
        public bool TicketVerified { get; set; } = false;
        public int ScreeningId { get; set; }
    }

    public class TicketVerifyDto 
    {
        public bool TicketVerified { get; set; } = true;
    }
}
