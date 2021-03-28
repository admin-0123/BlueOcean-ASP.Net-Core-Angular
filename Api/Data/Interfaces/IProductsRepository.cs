using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtaApi.Models;

namespace VirtaApi.Data.Interfaces
{
    public interface IProductsRepository : IBaseRepository
    {
        Task<Product> GetProduct(Guid id);
        Task<List<Product>> GetProducts();
    }
}
