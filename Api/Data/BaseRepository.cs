using System;
using System.Threading.Tasks;
using VirtaApi.Data.Interfaces;

namespace VirtaApi.Data
{
    public class BaseRepository : IBaseRepository
    {
        private readonly DataContext _context;
        public BaseRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
