using MediatR;
using MentorStudent.Application.Interfaces;
using MentorStudent.Domain.Aggregates;
using MentorStudent.Domain.Enums;
using MentorStudent.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.EventHandlers
{
    public class MessageSentEventHandler : INotificationHandler<MessageSentEvent>
    {
        private readonly IChatRepository _chatRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ISignalRNotifier _signalR;

        public MessageSentEventHandler(IChatRepository chatRepository, INotificationRepository notificationRepository, ISignalRNotifier signalR)
        {
            _chatRepository = chatRepository;
            _notificationRepository = notificationRepository;
            _signalR = signalR;
        }

        public async Task Handle(MessageSentEvent content, CancellationToken ct)
        {
            var chat = await _chatRepository.GetByIdAsync(content.ChatId);
            if (chat == null) return;
            var receivedid = content.SenderId == chat.MentorId ? chat.StudentId : chat.MentorId;

            var notif = new Notification(
                receivedid,
                NotificationType.MessageReceived,
                content.Content
                );
            await _notificationRepository.AddAsync(notif);
            await _signalR.PushNewMessage(receivedid);
            await _signalR.PushUnreadCount(receivedid);
        }
            
    }
}
