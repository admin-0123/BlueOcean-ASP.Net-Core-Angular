using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Virta.Data.Interfaces;
using Virta.Entities;

namespace Virta.Data
{
    public class ProductsRepository : BaseRepository<Product>, IProductsRepository
    {
        private readonly DataContext _context;

        public ProductsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetProduct(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetProducts(List<string> categories)
        {
            return await _context.Products.Where(p => p.Categories.Where(c => categories.Contains(c.Name)).Any()).ToListAsync();
        }
    }
}
