using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Virta.Entities;

namespace Virta.Data.Interfaces
{
    public interface IProductsRepository : IBaseRepository<Product>
    {
        Task<Product> GetProduct(Guid id);
        Task<List<Product>> GetProducts(int amount = 10);
        Task<List<Product>> GetProducts(string[] categories, int amount = 10);
    }
}
