using MediatR;
using MentorStudent.Application.Interfaces;

namespace MentorStudent.Application.Commands
{
    public class MarkNotificationsAsReadCommandHandler : IRequestHandler<MarkNotificationsAsReadCommand>
    {
        private readonly INotificationRepository _notificationRepository;

        public MarkNotificationsAsReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task Handle(MarkNotificationsAsReadCommand request, CancellationToken cancellationToken)
        {
            await _notificationRepository.MarkAllAsReadAsync(request.UserId);
        }
    }
}