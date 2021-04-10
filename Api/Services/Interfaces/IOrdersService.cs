using System.Threading.Tasks;
using Virta.Api.DTO;

namespace Virta.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<bool> UpsertOrder(OrderUpsert order);
    }
}
