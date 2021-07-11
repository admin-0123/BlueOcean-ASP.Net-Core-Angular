using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Virta.Extensions;

namespace Virta.Api.SignalR
{
    public class CustomerHub : Hub<ICustomerClient>
    {
        public static ConcurrentDictionary<Guid, List<string>> ConnectedCustomers = new ConcurrentDictionary<Guid, List<string>>();

        public override Task OnConnectedAsync()
        {
            var userName = Context.User.GetUserId();

            List<string> existingUserConnectionIds;
            ConnectedCustomers.TryGetValue(userName, out existingUserConnectionIds);

            if(existingUserConnectionIds == null)
            {
                existingUserConnectionIds = new List<string>();
            }

            existingUserConnectionIds.Add(Context.ConnectionId);

            ConnectedCustomers.TryAdd(userName, existingUserConnectionIds);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userName = Context.User.GetUserId();

            List<string> existingUserConnectionIds;
            ConnectedCustomers.TryGetValue(userName, out existingUserConnectionIds);

            existingUserConnectionIds.Remove(Context.ConnectionId);

            if(existingUserConnectionIds.Count == 0)
            {
                List<string> garbage;
                ConnectedCustomers.TryRemove(userName, out garbage);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
