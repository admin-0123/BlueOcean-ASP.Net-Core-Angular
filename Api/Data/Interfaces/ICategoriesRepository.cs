using System.Collections.Generic;
using System.Threading.Tasks;
using Virta.Entities;

namespace Virta.Data.Interfaces
{
    public interface ICategoriesRepository : IBaseRepository
    {
        Task<Category> GetCategory(string category);
        Task<List<Category>> GetCategories();
        Task<List<Category>> GetCategories(string[] categoryTitles);
        Task<List<Category>> GetCategoriesAC(string category);
        Task<List<Category>> GetCategoriesAC(string category, int amount);
    }
}
