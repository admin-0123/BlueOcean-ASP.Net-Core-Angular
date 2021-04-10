using System.Threading.Tasks;
using Virta.Models;

namespace Virta.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> UpsertProduct(ProductUpsert product);
    }
}
