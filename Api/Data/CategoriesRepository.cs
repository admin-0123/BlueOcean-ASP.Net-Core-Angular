using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Virta.Data.Interfaces;
using Virta.Entities;

namespace Virta.Data
{
    public class CategoriesRepository : BaseRepository, ICategoriesRepository
    {
        private readonly DataContext _context;
        public CategoriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetCategories(string[] categoryTitles)
        {
             return await _context.Categories.Where(c => categoryTitles.Contains(c.Title)).ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesAC(string category = "")
        {
            return await GetCategoriesAC(category, 10);
        }

        public async Task<List<Category>> GetCategoriesAC(string category = "", int amount = 10)
        {
            return await _context.Categories.Where(c => c.Title.StartsWith(category)).OrderBy(c => c.Title).Take(amount).ToListAsync();
        }

        public async Task<Category> GetCategory(string category)
        {
            return await _context.Categories.FirstAsync(c => c.Value == category);
        }
    }
}
