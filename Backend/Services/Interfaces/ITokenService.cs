using System.Threading.Tasks;
using Virta.Entities;

namespace Virta.Api.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateAsync(User user);
    }
}
