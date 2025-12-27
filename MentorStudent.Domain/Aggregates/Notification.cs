using MentorStudent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Domain.Aggregates
{
    public class Notification
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public NotificationType Type { get; private set; }
        public string Message { get; private set; }
        public bool isRead { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Notification() { }
        public Notification(Guid userId, NotificationType type, string message)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Type = type;
            Message = message;
            isRead = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsRead() => isRead = true;

    }
}
