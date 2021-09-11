using System.Collections.Generic;
using System.Threading.Tasks;
using Virta.Entities;

namespace Virta.Data.Interfaces
{
    public interface IAttributesRepository : IBaseRepository<Attribute>
    {
        Task<Attribute> GetAttribute(int Id);
        Task<Attribute> GetAttribute(string Name);
        Task<List<Attribute>> GetAttributes(string order = "ASC", int amount = 10);
    }
}
