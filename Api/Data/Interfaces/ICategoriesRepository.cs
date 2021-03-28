using System.Collections.Generic;
using System.Threading.Tasks;
using VirtaApi.Models;

namespace VirtaApi.Data.Interfaces
{
    public interface ICategoriesRepository : IBaseRepository
    {
        Task<List<Category>> GetCategoriesAC(string category);
        Task<List<Category>> GetCategoriesAC(string category, int amount);
    }
}
