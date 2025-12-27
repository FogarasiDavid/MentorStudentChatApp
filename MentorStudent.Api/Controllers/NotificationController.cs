using MediatR;
using MentorStudent.Application.Commands;
using MentorStudent.Application.Queries; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MentorStudent.Api.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("unread")]
        public async Task<IActionResult> GetUnread()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var query = new GetUnreadNotificationsQuery(userId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [HttpPut("mark-read")]
        public async Task<IActionResult> MarkAsRead()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

            await _mediator.Send(new MarkNotificationsAsReadCommand(userId));

            return NoContent();
        }
    }
}