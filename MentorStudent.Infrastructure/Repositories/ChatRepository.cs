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
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Chat?> GetByUsersAsync(Guid mentorId, Guid studentId)
        {
            return await _context.Chats.FirstOrDefaultAsync(c =>
                    c.MentorId == mentorId && c.StudentId == studentId
                );
        }
        public async Task<Chat?> GetByIdAsync(Guid id)
        {
            return await _context.Chats.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task AddAsync(Chat chat)
        {
            await _context.AddAsync(chat);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId)
        {
            return await _context.Chats
                .Where(c => c.MentorId == userId || c.StudentId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
