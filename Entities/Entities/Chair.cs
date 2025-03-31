using System.ComponentModel.DataAnnotations.Schema;

namespace Jegymester.DataContext.Entities
{
    public class Chair
    {
        public int Id {  get; set; } //tartalmaz 2 objektumot, (nem szoba-) sor-oszlop
        public int RoomId { get; set; } // foreign key
        public Room Room { get; set; }
        public bool IsReserved { get; set; } // false - nem foglalt, true - foglalt
    }
}
