using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{


    public class SignalRService
    {
        private readonly HubConnection _hubConnection;

        public SignalRService()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:7148/notificationHub") // API URL
                .Build();

            _hubConnection.On<string, string>("ReceiveNotification", (user, message) =>
            {
                // Handle received notification
                Console.WriteLine($"Notification received from {user}: {message}");
            });
        }

        public async Task StartAsync()
        {
            await _hubConnection.StartAsync();
        }

        public async Task SendNotificationAsync(string user, string message)
        {
            await _hubConnection.InvokeAsync("SendNotification", user, message);
        }
    }
}
