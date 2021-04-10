using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Virta.Entities
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
