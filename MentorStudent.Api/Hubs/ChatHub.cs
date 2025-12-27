using MediatR;
using MentorStudent.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace MentorStudent.Infrastructure.SignalR
{
    [Authorize] 
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ChatHub(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task JoinChat(Guid chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task SendMessage(Guid chatId, string content)
        {
            var userIdString = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdString, out var senderId))
            {
                _logger.LogWarning("ChatHub hiba: Azonosítatlan felhasználó próbált üzenetet küldeni.");
                throw new HubException("Azonosítatlan felhasználó.");
            }
            try
            {
                _logger.LogInformation("SignalR üzenet érkezett. Chat: {ChatId}, Felhasználó: {UserId}", chatId, senderId);

                var command = new SendMessageCommand(chatId, senderId, content);

                await _mediator.Send(command);
                await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage");

                _logger.LogInformation("SignalR üzenet sikeresen továbbítva a csoportnak.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hiba a SendMessage közben a Hub-ban!");
                throw; 
            }
        }

        public async Task LeaveChat(Guid chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}