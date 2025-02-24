namespace Jegymester.Properties
{
    public class Chair
    {
        public int CharId {  get; set; }
        public int Collumm { get; set; }
        public int Row { get; set; }
        public bool Reserved { get; set; }
        public List<RoomsChairs> RoomsChairs { get; set; }
      
    }
}
