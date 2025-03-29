using Jegymester.DataContext.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Jegymester.Dtos
{
    public class ScreeningDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime ScreeningTime { get; set; }
        public DateTime ScreeningstartTime { get; set; }
        public MovieDto Movie { get; set; }
        public IEnumerable<TicketDto> Tickets { get; set; }
    }
    public class ScreeningUpdateDto
    {
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime ScreeningTime { get; set; }
        public DateTime ScreeningstartTime { get; set; }
    }
    public class ScreeningCreateDto
    {
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime ScreeningTime { get; set; }
        public DateTime ScreeningstartTime { get; set; }
    }
    public class ScreeningDetailsDto
    {
        public int Id { get; set; }
        public DateTime ScreeningTime { get; set; }
        public DateTime ScreeningstartTime { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int RoomId { get; set; }
    }
}
