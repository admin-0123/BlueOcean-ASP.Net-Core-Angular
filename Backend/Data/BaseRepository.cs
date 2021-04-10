using System;
using System.Threading.Tasks;
using Virta.Data.Interfaces;

namespace Virta.Data
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;

        }
        public virtual void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public virtual void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public virtual void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
