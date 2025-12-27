using MentorStudent.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Domain.Aggregates
{
    public class Chat
    {
        private readonly List<Message> _messages = new();

        public Guid Id { get; private set; }
        public Guid MentorId { get; private set; }
        public Guid StudentId { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private readonly List<IDomainEvent> _domainEvents = new();
        private Chat() { }
        public Chat(Guid mentorId, Guid studentId)
        {
            if(mentorId == studentId)
            {
                throw new InvalidOperationException("Mentor és a tanulónak muszáj más felhasználónak lennie");

            }
            Id = Guid.NewGuid();
            MentorId = mentorId;
            StudentId = studentId;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void SendMessage(Guid senderId, string content)
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("A chat inaktív, és nemlehet küldeni bele üzenetet");
            }
            if (senderId != MentorId && senderId != StudentId)
            {
                throw new InvalidOperationException("A küldő nem részese a beszélgetésnek");
            }
            var message = new Message(Id, senderId, content);
            _messages.Add(message);
            _domainEvents.Add(new MessageSentEvent(
                
                Id,
                senderId,
                content,
                message.SentAt
                ));
        }

        public void Close() => IsActive = false;
        public void ClearDomainEvents() => _domainEvents.Clear();

    }
}
