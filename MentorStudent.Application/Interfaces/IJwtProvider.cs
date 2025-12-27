using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorStudent.Domain.Entities;

namespace MentorStudent.Application.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
