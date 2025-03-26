namespace Jegymester.DataContext.Entities
{
    public class Chair
    {
        public int Id {  get; set; } //ChairId
        public int Collumm { get; set; }
        public int Row { get; set; }
        public List<RoomChair> RoomsChairs { get; set; }
      
    }
}
