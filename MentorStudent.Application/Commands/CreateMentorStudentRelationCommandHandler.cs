using MediatR;
using MentorStudent.Application.Interfaces;
using MentorStudent.Domain.Aggregates;
using MentorStudent.Domain.Entities;
using MentorStudent.Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorStudent.Application.Commands
{
    public class CreateMentorStudentRelationCommandHandler
        :IRequestHandler<CreateMentorStudentRelationCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly ILogger<CreateMentorStudentRelationCommandHandler> _logger;
        public CreateMentorStudentRelationCommandHandler(IUserRepository userRepository, IChatRepository chatRepository, ILogger<CreateMentorStudentRelationCommandHandler> logger)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateMentorStudentRelationCommand command,CancellationToken ct)
        {
            _logger.LogInformation("Új Chat létrehozása indult. Mentor: {MentorId}, Diák: {StudentId}", command.mentorId, command.studentId);
            var mentor =  await _userRepository.GetByIdAsync(command.mentorId);
            var student = await _userRepository.GetByIdAsync(command.studentId);

            if(mentor?.Role != UserRole.Mentor )
            {
                throw new InvalidOperationException("A felhasználó nem mentor!");
            }
            if (student?.Role != UserRole.Student)
            {
                throw new InvalidOperationException("A felhasználó nem tanuló!");
            }
            var existingchat = await _chatRepository.GetByUsersAsync(command.studentId,command.mentorId);
            if (existingchat != null)
            {
                _logger.LogWarning("Chat létrehozása sikertelen: Már létezik beszélgetés. ({MentorId} - {StudentId})", command.mentorId, command.studentId);
                throw new InvalidOperationException("A beszélgetés már létezik");
            }

            var chat = new Chat(command.mentorId,command.studentId);
            _logger.LogInformation("Chat sikeresen létrehozva! ID: {ChatId}", chat.Id);
            await _chatRepository.AddAsync(chat);

            return chat.Id;

        }
    }
}
