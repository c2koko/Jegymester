
using Jegymester.DataContext.Entities;
using System.ComponentModel.DataAnnotations;

namespace Jegymester.DataContext.Dtos
{
    public class ScreeningDto
    {
        public int Id { get; set; }
        public string ScreeningLocation { get; set; }
        public DateTime ScreeningStartTime { get; set; }
        public MovieDto Movie { get; set; }

        //public IEnumerable<TicketDto> Tickets { get; set; }
    }

    public class ScreeningCreateDto
    {
        //id auto generated
        public string ScreeningLocation { get; set; }
        public DateTime ScreeningStartTime { get; set; }
        public int MovieId { get; set; }

        //public IEnumerable<TicketDto> Tickets { get; set; }
    }

    
    public class ScreeningUpdateDto
    {
        public string ScreeningLocation { get; set; }
        public DateTime ScreeningStartTime { get; set; }
        public int MovieId { get; set; }
        //public IEnumerable<TicketDto> Tickets { get; set; }
    }

    /*
    public class ScreeningDetailsDto
    {
        public int Id { get; set; }
        public DateTime ScreeningTime { get; set; }
        public DateTime ScreeningstartTime { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int RoomId { get; set; }
    }
    */
}


