using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace MentorStudent.Infrastructure.SignalR
{
    [Authorize]
    public class NotificationHub : Hub
    {

    }
}
