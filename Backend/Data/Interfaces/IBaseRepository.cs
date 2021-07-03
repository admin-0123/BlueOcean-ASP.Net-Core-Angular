using System;
using System.Threading.Tasks;

namespace Virta.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> SaveAll();
    }
}
