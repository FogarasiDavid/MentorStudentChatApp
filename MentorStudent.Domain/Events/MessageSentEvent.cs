using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;


namespace MentorStudent.Domain.Events
{
    public sealed class MessageSentEvent : IDomainEvent, INotification
    {
        public Guid ChatId { get;}
        public Guid SenderId { get; }
        public string Content { get; }
        public DateTime Timelaps {  get; }
        public DateTime OccuredAt => Timelaps;

        public MessageSentEvent(Guid chatId, Guid senderId, string content, DateTime timeLaps)
        {
            ChatId = chatId;
            SenderId = senderId;
            Content = content;
            Timelaps = timeLaps;
        }


    }
}
