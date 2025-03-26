namespace Jegymester.DataContext.Entities
{
    public class Ticket
    {
        public int Id { get; set; } //TicketId
        public DateTime Date { get; set; }
        public int Price { get; set; }

        //Egy ticket, egy vásárlás

        // A UserId szükséges, mint idegen kulcs
        public int UserId { get; set; }
        public User User { get; set; }
        //Egy ticket, Egy screening
        // A ScreeningId kell, mivel idegen kulcs
        public int ScreeningId { get; set; }
        public Screening Screening { get; set; }

    }
}
