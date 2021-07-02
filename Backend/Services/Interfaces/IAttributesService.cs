using System.Threading.Tasks;
using Virta.Models;

namespace Virta.Services.Interfaces
{
    public interface IAttributesService
    {
        Task<bool> UpsertAttribute(AttributeUpsert attribute);
    }
}
