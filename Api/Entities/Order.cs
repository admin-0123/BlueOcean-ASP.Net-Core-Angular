using System.Collections.Generic;

namespace Virta.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual User User { get; set; }
    }
}
