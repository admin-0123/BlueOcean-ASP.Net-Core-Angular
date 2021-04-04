using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Virta.Entities
{
    public class User : IdentityUser
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
