using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MentorStudent.Application.Interfaces;
using MentorStudent.Application.Queries;

namespace MentorStudent.Application.Queries
{
    public class GetMyChatsQueryHandler : IRequestHandler<GetMyChatsQuery, IEnumerable<ChatDto>>
    {
        private readonly IChatRepository _chatRepository;

        public GetMyChatsQueryHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<IEnumerable<ChatDto>> Handle(GetMyChatsQuery request, CancellationToken cancellationToken)
        {
            var chats = await _chatRepository.GetChatsByUserIdAsync(request.UserId);

            return chats.Select(chat =>
            {
                var partnerId = chat.MentorId == request.UserId ? chat.StudentId : chat.MentorId;

                return new ChatDto(chat.Id, partnerId, chat.CreatedAt);
            });
        }
    }
}