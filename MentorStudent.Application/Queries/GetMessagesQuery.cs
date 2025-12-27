using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Queries
{
    public record GetMessagesQuery(Guid ChatId, Guid RequestingUserId) : IRequest<IReadOnlyList<MessageDto>>;
}
