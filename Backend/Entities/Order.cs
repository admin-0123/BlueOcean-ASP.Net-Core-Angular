using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Virta.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal ShippingCost { get; set; }
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual User User { get; set; }
        public virtual Address Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
