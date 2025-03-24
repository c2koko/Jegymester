namespace Jegymester.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; } //TicketId
        public DateTime Date { get; set; }
        public int Price { get; set; }

        public int UserId { get; set; }
        public int ScreeningId { get; set; }
    }

}
