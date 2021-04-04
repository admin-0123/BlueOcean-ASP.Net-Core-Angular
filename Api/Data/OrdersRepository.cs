using System.Threading.Tasks;
using Virta.Data.Interfaces;
using Virta.Entities;

namespace Virta.Data
{
    public class OrdersRepository : BaseRepository, IOrdersRepository
    {
        private readonly DataContext _context;

        public OrdersRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder(int Id)
        {
            return await _context.Orders.FindAsync(Id);
        }
    }
}
