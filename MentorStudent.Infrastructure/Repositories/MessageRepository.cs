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
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public MessageRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Message>> GetByChatIdAsync(Guid chatId)
        {
            return await _context.Messages
                .Where(c => c.ChatId == chatId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}
