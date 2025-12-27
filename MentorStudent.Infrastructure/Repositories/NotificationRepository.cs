using MentorStudent.Application.Interfaces;
using MentorStudent.Domain.Aggregates;
using MentorStudent.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository 
    {
        private readonly ApplicationDbContext _context;
        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Notification>> GetUnreadByUserAsync(Guid userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && !n.isRead) 
                .OrderBy(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAllAsReadAsync(Guid userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.isRead)
                .ToListAsync();

            if (!notifications.Any()) return;

            foreach (var notif in notifications)
            {
                notif.MarkAsRead(); 
            }

            await _context.SaveChangesAsync();
        }
    }
}