using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Commands
{
    public record CreateMentorStudentRelationCommand(Guid studentId, Guid mentorId) : IRequest<Guid>;

}
