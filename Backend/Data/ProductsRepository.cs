using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Product>> GetProducts(int amount = 10)
        {
            return await _context.Products
                .OrderByDescending(p => p.CreatedAt).Take(amount).ToListAsync();
        }

        public async Task<List<Product>> GetProducts(string[] categories, int amount = 10)
        {
            if (categories.Length > 0)
                return await _context.Products.Where(
                        p => p.Categories.Where(c => categories.Contains(c.Name)).Any()
                    ).OrderByDescending(p => p.CreatedAt).Take(amount).ToListAsync();

            return await GetProducts(amount);
        }
    }
}
