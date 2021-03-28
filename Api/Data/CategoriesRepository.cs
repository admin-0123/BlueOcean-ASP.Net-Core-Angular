using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtaApi.Data.Interfaces;
using VirtaApi.Models;

namespace VirtaApi.Data
{
    public class CategoriesRepository : BaseRepository, ICategoriesRepository
    {
        private readonly DataContext _context;
        public CategoriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAC(string category = "")
        {
            return await GetCategoriesAC(category, 10);
        }

        public async Task<List<Category>> GetCategoriesAC(string category = "", int amount = 10)
        {
            return await _context.Categories.Where(c => c.Title.StartsWith(category)).OrderBy(c => c.Title).Take(amount).ToListAsync();
        }

        public async Task<Category> GetCategory(Category category)
        {
            return await _context.Categories.FindAsync(category);
        }
    }
}
