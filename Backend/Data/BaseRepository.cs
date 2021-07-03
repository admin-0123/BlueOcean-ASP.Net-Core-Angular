using System;
using System.Threading.Tasks;
using Virta.Data.Interfaces;

namespace Virta.Data
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }
        public virtual void Add(T entity)
        {
            _context.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            _context.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
