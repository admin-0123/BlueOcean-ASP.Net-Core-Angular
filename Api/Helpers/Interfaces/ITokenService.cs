using System.Threading.Tasks;
using VirtaApi.Models;

namespace VirtaApi.Helpers.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
