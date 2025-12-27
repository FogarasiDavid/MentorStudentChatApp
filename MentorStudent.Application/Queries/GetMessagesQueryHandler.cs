using MediatR;
using MentorStudent.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Queries
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery,IReadOnlyList<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;

        public GetMessagesQueryHandler(IMessageRepository messageRepository, IChatRepository chatRepository)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
        }

        public async Task<IReadOnlyList<MessageDto>> Handle(GetMessagesQuery content, CancellationToken ct)
        {
            var chat = await _chatRepository.GetByIdAsync(content.ChatId)
                ?? throw new InvalidOperationException("Nem létezik ez a beszélgetés");
            if (content.RequestingUserId != chat.MentorId
                && content.RequestingUserId != chat.StudentId)
                throw new UnauthorizedAccessException();

            var messages = await _messageRepository.GetByChatIdAsync(content.ChatId);

            return messages
                .Select(m => new MessageDto(
                    m.Id,
                    m.SenderId,
                    m.Content,
                    m.SentAt
                    )).ToList();
        }
    }
}
