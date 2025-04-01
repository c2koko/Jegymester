using Jegymester.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.DataContext.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } //kesobb átírni passwordHash-re
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Ticket> Tickets { get; set; }
    }

    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; } //kesobb átírni passwordHash-re
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
    }

    public class UserLoginDto 
    {
        //szerintem elég csak email + password / username + password
        public string Email { get; set; }
        public string Username { get; set; } 
        public string Password { get; set; }
    }

    public class UserUpdateDto 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }


}

/* ============================================= UNDER DEV =========================================

using System.ComponentModel.DataAnnotations;

// dto a netpincérből

namespace NetpincerApp.DataContext.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public IList<RoleDto> Role { get; set; }
        //public IList<AddressDto> Addresses { get; set; }
    }

    public class UserRegisterDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public IList<int> RoleIds { get; set; }
    }

    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public IList<int> RoleIds { get; set; }
    }  
}

============================================= UNDER DEV ========================================= */

