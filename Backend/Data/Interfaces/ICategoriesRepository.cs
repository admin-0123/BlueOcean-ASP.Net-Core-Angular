using System.Collections.Generic;
using System.Threading.Tasks;
using Virta.Entities;

namespace Virta.Data.Interfaces
{
    public interface ICategoriesRepository : IBaseRepository<Category>
    {
        Task<Category> GetCategory(int id);
        Task<Category> GetCategory(string category);
        Task<List<Category>> GetCategories(int amount = 10);
        Task<List<Category>> GetCategoriesByName(string order = "ASC", int amount = 10);
    }
}
