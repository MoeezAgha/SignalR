using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIApplication
{
    public class MessageHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();

        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageToUser(string targetUser, string user, string message)
        {
            if (_connections.TryGetValue(targetUser, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", user, message);
            }
        }

        public override Task OnConnectedAsync()
        {
            var userName = Context.GetHttpContext().Request.Query["userName"].ToString();
            if (!string.IsNullOrEmpty(userName))
            {
                _connections[userName] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userName = Context.GetHttpContext().Request.Query["userName"].ToString();
            if (!string.IsNullOrEmpty(userName))
            {
                _connections.TryRemove(userName, out _);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public static List<string> GetOnlineUsers()
        {
            return new List<string>(_connections.Keys);
        }

        public static string? GetConnectionId(string userName)
        {
            _connections.TryGetValue(userName, out var connectionId);
            return connectionId;
        }
    }
}
