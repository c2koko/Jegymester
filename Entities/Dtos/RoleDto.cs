using Jegymester.DataContext.Entities;

namespace Jegymester.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; } 
        public required int PermaId { get; set; } 
        public required string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
