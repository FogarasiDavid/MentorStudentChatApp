using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Interfaces
{
    public interface IPasswordHash
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}