using MediatR;
using MentorStudent.Application.Commands;
using MentorStudent.Application.Queries;
using MentorStudent.Infrastructure.SignalR; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR; 
using System.Security.Claims;

namespace MentorStudent.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IMediator mediator, IHubContext<ChatHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyChats()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

            var query = new GetMyChatsQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetMessages(Guid id)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

            var query = new GetMessagesQuery(id, userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("messages")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand clientData)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var realUserId))
            {
                return Unauthorized();
            }

            var safeCommand = new SendMessageCommand(clientData.ChatId, realUserId, clientData.Content);

            await _mediator.Send(safeCommand);

            await _hubContext.Clients.All.SendAsync("ReceiveMessage");

            return Ok();
        }
    }
}