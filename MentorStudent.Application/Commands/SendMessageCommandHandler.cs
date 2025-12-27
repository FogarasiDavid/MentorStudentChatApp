using MediatR;
using MentorStudent.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Commands
{
    public class SendMessageCommandHandler
    : IRequestHandler<SendMessageCommand>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMediator _mediator;

        public SendMessageCommandHandler(IChatRepository chatRepository, IMessageRepository messageRepository, IMediator mediator)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _mediator = mediator;
        }

        public async Task Handle (SendMessageCommand command, CancellationToken ct)
        {
            var chat = await _chatRepository.GetByIdAsync (command.ChatId)
                     ?? throw new InvalidOperationException("A beszélgetés nem található");
            chat.SendMessage(command.SenderId, command.Content);

            var message = chat.Messages.Last();
            await _messageRepository.AddAsync (message);
            foreach (var domainEvent in chat.DomainEvents)
            {
                await _mediator.Publish (domainEvent, ct);
            }
            chat.ClearDomainEvents();
        }
    }
}
