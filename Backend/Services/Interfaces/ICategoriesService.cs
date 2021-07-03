using System.Threading.Tasks;
using Virta.Models;

namespace Virta.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<bool> Upsert(CategoryUpsert category);
    }
}
