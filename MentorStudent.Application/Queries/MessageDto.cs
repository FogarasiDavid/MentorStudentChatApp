using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Queries
{
    public record MessageDto(Guid Id, Guid SenderId, string Content, DateTime SentAt);
}
