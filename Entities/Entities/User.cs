namespace Jegymester.Entities
{
    public class User
    {
        public int Id {  get; set; } //UserId
        public string Name {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        // Egy ember egy role

       /*
        * A RoleId property azért kell, hogy idegen kulcsént
        * meg lehessen adni a User-hez tartozó Role-t, ami aztán
        * a Role property-ben lessz referenciaként
        */
        public int RoleId { get; set; }
        public Role Role {  get; set; }

        //Egy user több ticket
        
        public List<Ticket> Tickets { get; set; }

    }
}
