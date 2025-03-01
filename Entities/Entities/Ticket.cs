namespace Jegymester.Entities
{
    public class Ticket
    {
        public int Id { get; set; } //TicketId
        public DateTime date { get; set; }
        public int Price { get; set; }

        //Egy ticket, egy vásárlás
        public Users User { get; set; }
        //Egy ticket, Egy screening
        public Screening Screening { get; set; }

    }
}
