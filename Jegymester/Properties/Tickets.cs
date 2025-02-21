namespace Jegymester.Properties
{
    public class Tickets
    {
        public int TicketId { get; set; }
        public DateTime date { get; set; }
        public int Price { get; set; }

        //Egy ticket....?
        public List<PurchasedTickets> purchasedTickets { get; set; } = new List<PurchasedTickets>();

    }
}
