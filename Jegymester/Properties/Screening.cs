namespace Jegymester.Properties
{
    public class Screening
    {
        public int ScreeningId { get; set; }
        
        //Egy screeninghez egy Movie
        public Movies Movie { get; set; }
    }
}
