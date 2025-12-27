using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Queries
{
    public record ChatDto(Guid ChatId, Guid PartnerId, DateTime CreatedAt);
}
