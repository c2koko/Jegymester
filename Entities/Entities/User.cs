using System.Net.Sockets;

namespace Jegymester.DataContext.Entities
{
    public class User
    {
        public int Id {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; } //kesobb átírni passwordHash-re
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //---------------1 role/user
        public int RoleId { get; set; } //FK - ez kapcsolja a rolet a userhez
        public Role Role { get; set; }

        //---------------több ticket/user
        public List<Ticket> Tickets { get; set; }
    }
}
