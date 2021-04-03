using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Virta.Models;

namespace Virta.Data.Interfaces
{
    public interface IProductsRepository : IBaseRepository
    {
        Task<Product> GetProduct(Guid id);
        Task<List<Product>> GetProducts();
        Task<List<Product>> GetProducts(List<string> categories);
    }
}
