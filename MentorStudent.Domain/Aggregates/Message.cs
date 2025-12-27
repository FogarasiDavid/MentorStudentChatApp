using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Domain.Aggregates
{
    public class Message
    {
        public Guid Id { get; private set; }
        public Guid ChatId { get; private set; }
        public Guid SenderId { get; private set; }
        public string Content { get; private set; }
        public DateTime SentAt { get; private set; }

        private Message() { }

        public Message(Guid chatId, Guid senderId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Üres üzenet");

            Id = Guid.NewGuid();
            ChatId = chatId;
            SenderId = senderId;
            Content = content;
            SentAt = DateTime.UtcNow;
        }
    }

}
