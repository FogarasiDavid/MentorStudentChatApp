using MentorStudent.Domain.Aggregates;
using MentorStudent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat?> GetByUsersAsync(Guid mentorId, Guid studentId);
        Task<Chat?> GetByIdAsync(Guid id);
        Task AddAsync(Chat chat);
        Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId);

    }
}
