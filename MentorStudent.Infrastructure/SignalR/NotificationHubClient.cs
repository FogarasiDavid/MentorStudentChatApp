using MentorStudent.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MentorStudent.Infrastructure.SignalR
{
    public class NotificationHubClient : ISignalRNotifier
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationHubClient(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task PushNewMessage(Guid userId)
        {
            await _hubContext.Clients.User(userId.ToString())
                .SendAsync("ReceiveMessage");
        }

        public async Task PushUnreadCount(Guid userId)
        {
            await _hubContext.Clients.User(userId.ToString())
                .SendAsync("UpdateUnreadCount");
        }
    }
}