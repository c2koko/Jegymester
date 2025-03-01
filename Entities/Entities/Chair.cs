namespace Jegymester.Entities
{
    public class Chair
    {
        public int Id {  get; set; } //ChairId
        public int Collumm { get; set; }
        public int Row { get; set; }
        public List<RoomsChairs> RoomsChairs { get; set; }
      
    }
}
