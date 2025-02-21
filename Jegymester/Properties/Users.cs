namespace Jegymester.Properties
{
    public class Users
    {
        public int UserID {  get; set; }
        public string Name {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        
        // Egy ember egy role
        public Roles Role {  get; set; }
        
        //Egy vásárlónak több vásárlása
        public List<PurchasedTickets> purchasedTickets { get; set; } = new List<PurchasedTickets>();

    }
}
