/* ============================================= UNDER DEV =========================================*/
namespace Jegymester.DataContext.Entities
{
    public class Chair
    {
        public int Id { get; set; } //tartalmaz 2 objektumot, (nem szoba-) sor-oszlop
        public int ScreeningId { get; set; } // foreign key
        public Screening screening { get; set; }
        public bool IsReserved { get; set; } // false - nem foglalt, true - foglalt

        public int TicketId {  get; set; }

        public Ticket Ticket { get; set; }
    }
}
/*============================================= UNDER DEV =========================================*/