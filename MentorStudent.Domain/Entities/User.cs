using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorStudent.Domain.Entities;
using MentorStudent.Domain.Enums;
using MentorStudent.Domain.ValueObjects;

namespace MentorStudent.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public MentorProfile? MentorProfile { get; private set; }
        public StudentProfile? StudentProfile { get; private set; }

        private User() { }
        public User (Guid id, Email email, string passwordHash, UserRole role)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public void AttachMentorRole(MentorProfile profile)
        {
            if(Role != UserRole.Mentor)
            {
                throw new InvalidOperationException("A felhasználó nem mentor");
            }
            MentorProfile = profile;
        }

        public void AttachStudentRole(StudentProfile profile)
        {
            if (Role != UserRole.Student)
            {
                throw new InvalidOperationException("A felhasználó nem tanuló");
            }
            StudentProfile = profile;
        }

    }
}
