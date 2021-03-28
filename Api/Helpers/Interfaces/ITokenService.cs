using System.Threading.Tasks;
using VirtaApi.Models;

namespace VirtaApi.Helpers.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
