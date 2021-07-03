using System.Threading.Tasks;
using Virta.Entities;

namespace Virta.Data.Interfaces
{
    public interface IMongoBaseRepository<T> where T : MongoBaseDocument
    {
        Task<T> GetAsync(string Id);
        Task InsertAsync(T entity);
        Task DeleteAsync(string id);
    }
}
