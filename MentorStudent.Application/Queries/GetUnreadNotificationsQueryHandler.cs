using MediatR;
using MentorStudent.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Queries
{
    public class GetUnreadNotificationsQueryHandler : IRequestHandler<GetUnreadNotificationsQuery,IReadOnlyList<NotificationDto>>
    {
        private readonly INotificationRepository _notification;

        public GetUnreadNotificationsQueryHandler(INotificationRepository notification)
        {
            _notification = notification;
        }

        public async Task<IReadOnlyList<NotificationDto>> Handle(GetUnreadNotificationsQuery content, CancellationToken ct)
        {
            var notifications = await _notification.GetUnreadByUserAsync(content.UserId);
            if (notifications == null || !notifications.Any())
            {
                return new List<NotificationDto>(); 
            }
            return notifications
                .Select(n => new NotificationDto
                (
                  n.Id,
                  n.Message,
                  n.CreatedAt
                  )).ToList();
        }
    }
}
