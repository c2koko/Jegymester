namespace Jegymester.Properties
{
    public class Screening
    {
        public int ScreeningId { get; set; }
        public DateTime ScreeningTime { get; set; }
        public DateTime ScreeningstartTime { get; set; }
        
        //Egy screeninghez egy Movie
        public Movies Movie { get; set; }

        // Egy screeninghez több jegy
        public List<Tickets> Tickets { get; set; }

        //Egy vetítés Több terem
        public List<Rooms> Rooms { get; set; } 
        
    }
}
