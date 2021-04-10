using System.Collections.Generic;
using System.Threading.Tasks;
using Virta.Entities;

namespace Virta.Data.Interfaces
{
    public interface IOrdersRepository : IBaseRepository
    {
        Task<Order> GetOrder(int Id);
        Task<List<Order>> GetOrders();
    }
}
