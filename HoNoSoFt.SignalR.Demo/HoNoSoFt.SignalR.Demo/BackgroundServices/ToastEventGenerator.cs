using HoNoSoFt.SignalR.Demo.Hubs;
using HoNoSoFt.SignalR.Demo.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HoNoSoFt.SignalR.Demo.BackgroundServices
{
    public class ToastEventGenerator : BackgroundService
    {
        private readonly IHubContext<ToastHub> _toastHubContext;

        /// <summary>
        /// The random, mais vous devriez utiliser la nouvelle classe secure random.
        /// </summary>
        private static Random _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        public ToastEventGenerator(IHubContext<ToastHub> toastHubContext)
        {
            _toastHubContext = toastHubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await BroadcastToastNotificationAsync((ToastImportance)_random.Next(0, 3), "Toast random")
                    .ConfigureAwait(false);

                // Devrait être random
                await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);
            }
        }

        private Task BroadcastToastNotificationAsync(ToastImportance toastImportance, string message)
        {
            return _toastHubContext
                .Clients
                .All
                .SendCoreAsync("SendNotification", new[] { "System", toastImportance.ToString(), message });
        }
    }
}
