using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Virta.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public virtual string Firstname { get; set; }
        [Required]
        public virtual string Lastname { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
