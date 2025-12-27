using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Domain.Entities
{
    public class StudentProfile
    {
        public Guid UserId { get; private set; }
        public string Goal { get; private set; } = string.Empty;
        public string Level { get; private set; } = string.Empty;

        private StudentProfile() { }

        public StudentProfile(Guid userId, string goal, string level)
        {
            UserId = userId;
            Goal = goal;
            Level = level;
        }
    }

}
