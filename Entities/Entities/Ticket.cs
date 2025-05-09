
namespace Jegymester.DataContext.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int Price { get; set; }
        public bool TicketVerified { get; set; } = false; // a specifikacioban valami miatt kell

        //---------------1 user/ticket
        //Nullable mivel ha nem regisztrált flehasználó rendel akkor nincs userId-ja a 
        //a regisztreát felhasználó Id-jét a token-ből szedjük ki
        public int? UserId { get; set; }
        public User? User { get; set; }

        //---------------1 screening/ticket
        public int ScreeningId { get; set; } //FK - ez kapcsolja a screeninget a tickethez
        public Screening Screening { get; set; }

        public List <Chair> Chairs { get; set; }

    }
}
