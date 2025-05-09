namespace Jegymester.DataContext.Entities
{
    public class Screening
    {
        public int Id { get; set; }
        public required DateTime ScreeningStartTime { get; set; }
        //public string ScreeningLocation { get; set; }

        //---------------1 movie/screening
        public required int MovieId { get; set; } //FK - ez kapcsolja a moviet a screeninghez
        public Movie Movie { get; set; }

        //---------------több ticket/screening
        public List<Ticket> Tickets { get; set; }

        //---------------room stuff
        public int RoomId { get; set; }
        public Room Room { get; set; }

    }
}
