namespace Jegymester.Properties
{
    public class PurchasedTickets
    {
        public int TicketID { get; set; }
        public int UserID { get; set; }

        //Egy ticket egy vásárláshoz
        public Tickets Ticket { get; set; }
        //Egy vásárlás egy user
        public Users User { get; set; }
    }
}
