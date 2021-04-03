using Virta.Models;

namespace Virta.Api.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
