/* ============================================= UNDER DEV =========================================*/
namespace Jegymester.DataContext.Entities
{
    public class Chair
    {
        public int Id { get; set; } //tartalmaz 2 objektumot, (nem szoba-) sor-oszlop
        public int RoomId { get; set; } // foreign key
        public Room Room { get; set; }
        public bool IsReserved { get; set; } // false - nem foglalt, true - foglalt

        public int TicketId {  get; set; }

        public Ticket Ticket { get; set; }
    }
}
/*============================================= UNDER DEV =========================================*/