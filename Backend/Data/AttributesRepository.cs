using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Virta.Data.Interfaces;
using Virta.Entities;

namespace Virta.Data
{
    public class AttributesRepository : BaseRepository<Attribute>, IAttributesRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Attribute> _attributesDbSet;

        public AttributesRepository(DataContext context) : base(context)
        {
            _context = context;
            _attributesDbSet = _context.Attributes;
        }

        public async Task<Attribute> GetAttribute(int Id)
        {
            return await _attributesDbSet.FindAsync(Id);
        }

        public async Task<Attribute> GetAttribute(string Name)
        {
            return await _attributesDbSet.FirstAsync(a => a.Name == Name);
        }

        public async Task<List<Attribute>> GetAttributes(string order = "ASC", int amount = 10)
        {
            if (order == "ASC")
                return await _attributesDbSet.OrderBy(a => a.Name).Take(amount).ToListAsync();

            return await _attributesDbSet.OrderByDescending(a => a.Name).Take(amount).ToListAsync();
        }
    }
}
