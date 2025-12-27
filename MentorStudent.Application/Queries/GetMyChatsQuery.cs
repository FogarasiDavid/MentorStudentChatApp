using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MentorStudent.Application.Queries;

namespace MentorStudent.Application.Queries
{
    public record GetMyChatsQuery(Guid UserId) : IRequest<IEnumerable<ChatDto>>;
}