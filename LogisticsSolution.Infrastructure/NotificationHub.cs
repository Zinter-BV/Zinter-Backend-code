using LogisticsSolution.Application.Contract.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticsSolution.Infrastructure
{
    public class NotificationHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotification _realTime;

        public NotificationHub(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _realTime = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<INotification>();
        }
        public async override Task OnConnectedAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string userId = httpContext?.Request.Query["userId"];

            // Store the connection ID associated with this staffId
            if (!string.IsNullOrEmpty(userId))
            {
                _realTime.AddSignalRConnection(userId, Context.ConnectionId);
            }

            return;
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string userId = httpContext?.Request.Query["userId"];

            if (!string.IsNullOrEmpty(userId))
            {
                _realTime.RemoveSignalRConnection(userId);
            }

            return;
        }
    }
}
