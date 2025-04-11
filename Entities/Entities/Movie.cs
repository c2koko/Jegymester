using Microsoft.EntityFrameworkCore;

namespace Jegymester.DataContext.Entities
{
    
    public class Movie
    {
        public int Id {  get; set; }
        public required string MovieName {  get; set; }
        public string? MovieDescription { get; set; }
        public int MovieDuration { get; set; }

        //---------------1 moviehoz több screening
        public List<Screening> Screenings { get; set; }

        // film kép
        public string? MovieImg { get; set; }

    }
}
