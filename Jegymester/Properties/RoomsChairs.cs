namespace Jegymester.Properties
{
    public class RoomsChairs
    {
        public int RoomId { get; set; }
        public Rooms Room { get; set; }

        public int ChairId { get; set; }
        public Chair Chair { get; set; }

        public bool Reserved { get; set; }
    }
}
