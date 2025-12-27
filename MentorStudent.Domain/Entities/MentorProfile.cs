using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Domain.Entities
{
    public class MentorProfile
    {
        public Guid UserId { get; private set; }
        public string Bio { get; private set; } = string.Empty;
        public string Skills { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }

        private MentorProfile() { }

        public MentorProfile(Guid userId, string bio, string skills)
        {
            UserId = userId;
            Bio = bio;
            Skills = skills;
            IsActive = true;
        }

        public void Deactivate() => IsActive = false;
    }
}
