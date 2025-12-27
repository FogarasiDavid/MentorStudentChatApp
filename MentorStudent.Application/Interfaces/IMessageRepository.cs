using MentorStudent.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Interfaces
{
    public interface IMessageRepository
    {
        Task<IReadOnlyList<Message>> GetByChatIdAsync(Guid chatId);
        Task AddAsync(Message message);
    }
}
