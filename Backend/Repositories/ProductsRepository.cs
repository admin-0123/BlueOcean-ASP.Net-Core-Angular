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
            return await GetProducts(null, null, amount);
        }

        public async Task<List<Product>> GetProducts(string[] categories, int amount = 10)
        {
            return await GetProducts(categories, null, amount);
        }

        public async Task<List<Product>> GetProducts(string title, int amount = 10)
        {
            return await GetProducts(new string[] { }, title, amount);
        }

        public async Task<List<Product>> GetProducts(string[] categories, string title, int amount = 10)
        {
            var result = _context.Products.AsQueryable();

            if (categories != null && categories.Length > 0) {
                result = result.Where(p => p.Categories.Where(c => categories.Contains(c.Name)).Any());
            }

            if (title != null && title != "") {
                result = result.Where(p => p.Title.Contains(title)).OrderByDescending(p => p.Title);
            } else {
                result = result.OrderByDescending(p => p.CreatedAt);
            }

            return await result.Take(amount).ToListAsync();
        }
    }
}
