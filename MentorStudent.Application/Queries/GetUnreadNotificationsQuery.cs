using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace MentorStudent.Application.Queries;

public record GetUnreadNotificationsQuery(Guid UserId) : IRequest<IReadOnlyList<NotificationDto>>;

