namespace Jegymester.Entities
{
    public class Roles
    {
        public int Id { get; set; } //RoleId
        public string RoleName { get; set; }

        //Egy Role több User
        public List<Users> Users { get; set; } = new List<Users>();
    }
}
