using System.Runtime.CompilerServices;
namespace Jegymester.Entities
{
    public class Room
    {
        public int Id {  get; set; } //RoomId
        //Egy room-ban, több vetítés
        public List<Screening> Screenings { get; set; }
        public List<RoomChair> RoomsChairs { get; set; }
    }
}
