using System;
using System.ComponentModel.DataAnnotations;

namespace Virta.Entities
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string District { get; set; }
        [Required]
        [MaxLength(200)]
        public string AddressLine1 { get; set; }
        [MaxLength(200)]
        #nullable enable
        public string? AddressLine2 { get; set; }
        #nullable disable
        [Required]
        [MaxLength(50)]
        public string PostCode { get; set; }
        [Required]
        public bool Primary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User User { get; set; }
    }
}
