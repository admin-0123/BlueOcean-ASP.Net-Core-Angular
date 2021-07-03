using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Virta.Data.Interfaces;
using Virta.Entities;

namespace Virta.Data
{
    public class AttributesRepository : BaseRepository, IAttributesRepository
    {
        private readonly DataContext _context;

        public AttributesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Attribute> GetAttribute(int Id)
        {
            return await _context.Attributes.FindAsync(Id);
        }

        public async Task<Attribute> GetAttribute(string Name)
        {
            return await _context.Attributes.FirstAsync(a => a.Name == Name);
        }

        public async Task<List<Attribute>> GetAttributes(string order = "ASC", int amount = 10)
        {
            if (order == "ASC")
                return await _context.Attributes.OrderBy(a => a.Name).Take(amount).ToListAsync();

            return await _context.Attributes.OrderByDescending(a => a.Name).Take(amount).ToListAsync();
        }
    }
}
