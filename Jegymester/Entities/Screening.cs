namespace Jegymester.Entities
{
    public class Screening
    {
        public int Id { get; set; } //ScreeningId
        public DateTime ScreeningTime { get; set; }
        public DateTime ScreeningstartTime { get; set; }
        
        //Egy screeninghez egy Movie
        public Movies Movie { get; set; }

        // Egy screeninghez több jegy
        public List<Ticket> Tickets { get; set; }

        //Egy vetítés Egy teremben (Letároljuk az időt, így az adott időben lévő vetítés egyedi. Lehet, hogy lesz egy ugyanolyan film, de az másik időben)
       public Rooms Room {  get; set; }
        
    }
}
