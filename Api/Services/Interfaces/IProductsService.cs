using System.Threading.Tasks;
using Virta.MVC.ViewModels;
using Virta.Api.DTO;

namespace Virta.Services.Interfaces
{
    public interface IProductsService
    {
        Task<bool> UpsertProduct(ProductUpsert product);
    }
}
