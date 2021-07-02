using System.Collections.Generic;
using System.Threading.Tasks;
using Virta.Entities;

namespace Virta.Data.Interfaces
{
    public interface ICategoriesRepository : IBaseRepository
    {
        Task<Category> GetCategory(int id);
        Task<Category> GetCategory(string category);
        Task<List<Category>> GetCategories(string order = "ASC", int amount = 10);
    }
}
