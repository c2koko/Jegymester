namespace Jegymester.Properties
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        //Egy Role több User
        public List<Users> Users { get; set; } = new List<Users>();
    }
}
