using System;
using System.Threading.Tasks;

namespace Virta.Data.Interfaces
{
    public interface IBaseRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        Task<bool> SaveAll();
    }
}
