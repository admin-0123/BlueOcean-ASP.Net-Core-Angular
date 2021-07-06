using System;
using System.Threading.Tasks;
using Virta.Entities;

namespace Virta.Data.Interfaces
{
    public interface IMongoBaseRepository<T> where T : MongoBaseDocument
    {
        Task<T> GetAsync(Guid id);
        Task UpsertAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
