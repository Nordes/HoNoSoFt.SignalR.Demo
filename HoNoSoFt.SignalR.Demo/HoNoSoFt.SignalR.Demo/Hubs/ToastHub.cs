using System;
using System.Threading.Tasks;
using HoNoSoFt.SignalR.Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace HoNoSoFt.SignalR.Demo.Hubs
{
    //[Authorize]
    public class ToastHub : Hub
    {
        private readonly ILogger<ToastHub> _logger;

        public ToastHub(ILogger<ToastHub> logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            // For the context... we need the Authorize attribute to be set (Authentication).
            _logger.LogInformation($"{Context.User.Identity.Name} just joined.");

            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // For the context... we need the Authorize attribute to be set (Authentication).
            _logger.LogInformation($"{Context.User.Identity.Name} just left.");

            return Task.CompletedTask;
        }
    }
}
