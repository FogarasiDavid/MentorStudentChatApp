using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Interfaces
{
    public interface ISignalRNotifier
    {
        Task PushNewMessage(Guid userId);
        Task PushUnreadCount(Guid userId);
    }
}
