using MentorStudent.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<IReadOnlyList<Notification>> GetUnreadByUserAsync(Guid userId);
        Task MarkAllAsReadAsync(Guid userId);
    }
}
