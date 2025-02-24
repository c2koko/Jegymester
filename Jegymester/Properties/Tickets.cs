namespace Jegymester.Properties
{
    public class Tickets
    {
        public int TicketId { get; set; }
        public DateTime date { get; set; }
        public int Price { get; set; }

        //Egy ticket, egy vásárlás
        public Users User { get; set; }
        //Egy ticket, Egy screening
        public Screening Screening { get; set; }

    }
}
