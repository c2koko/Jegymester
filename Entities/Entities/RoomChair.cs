namespace Jegymester.Entities
{
    public class RoomChair
    {
        public int Id { get; set; } //RoomId
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int ChairId { get; set; }
        public Chair Chair { get; set; }
        public bool Reserved { get; set; }
    }
}
