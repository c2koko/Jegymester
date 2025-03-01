namespace Jegymester.Entities
{
    public class Users
    {
        public int Id {  get; set; } //UserId
        public string Name {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        
        // Egy ember egy role
        public Roles Role {  get; set; }

        //Egy user több ticket
        
        public List<Ticket> Tickets { get; set; }

    }
}
