namespace Jegymester.DataContext.Entities
{
    
    public class Movie
    {
        public int Id {  get; set; } //MovieId
        public string MovieName {  get; set; }
        public string? MovieDescription { get; set; }

        //Egy movie-hoz több screening
        public List<Screening>? Screenings { get; set; }
       
    }
}
