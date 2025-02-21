namespace Jegymester.Properties
{
    
    public class Movies
    {
        public int MovieId {  get; set; }
        public string MovieName {  get; set; }
        public string MovieDescription { get; set; }

        //Egy movie-hoz több screening
        public List<Screening> Screenings { get; set; }
       
    }
}
