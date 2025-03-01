namespace Jegymester.Entities
{
    public class RoomsChairs
    {
        public int Id { get; set; } //RoomId
        public Rooms Room { get; set; }

        public int ChairId { get; set; }
        public Chair Chair { get; set; }

        public bool Reserved { get; set; }
    }
}
