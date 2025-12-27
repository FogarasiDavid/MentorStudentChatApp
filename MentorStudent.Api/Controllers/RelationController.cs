using MediatR;
using MentorStudent.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentorStudent.Api.Controllers
{
    [Authorize] // Csak bejelentkezve
    [ApiController]
    [Route("api/[controller]")]
    public class RelationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRelation([FromBody] CreateMentorStudentRelationCommand command)
        {
            try
            {
                var chatId = await _mediator.Send(command);
                return Ok(new { ChatId = chatId, Message = "Kapcsolat és Chat szoba létrejött!" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}