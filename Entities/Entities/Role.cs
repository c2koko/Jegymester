namespace Jegymester.DataContext.Entities
{
    public class Role
    {
        public int Id { get; set; } //RoleId
        public string RoleName { get; set; } = "";

        //Egy Role több User
        public List<User> Users { get; set; }
    }
}
