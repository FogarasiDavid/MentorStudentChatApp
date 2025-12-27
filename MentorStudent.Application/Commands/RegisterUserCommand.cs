using MentorStudent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;


namespace MentorStudent.Application.Commands
{
    public record RegisterUserCommand(string Email, string Password, UserRole Role) : IRequest<string>;
}
