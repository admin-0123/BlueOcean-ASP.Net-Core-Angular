using System.Threading.Tasks;

namespace Virta.Api.SignalR
{
    public interface ICustomerClient
    {
        Task OnCartUpdate();
    }
}
