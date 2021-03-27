using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace VirtaApi.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
