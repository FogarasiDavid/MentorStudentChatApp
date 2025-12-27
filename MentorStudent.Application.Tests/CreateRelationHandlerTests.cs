using FluentAssertions;
using MentorStudent.Application.Commands;
using MentorStudent.Application.Interfaces;
using MentorStudent.Domain.Aggregates;
using MentorStudent.Domain.Entities;
using MentorStudent.Domain.Enums;
using MentorStudent.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;

namespace MentorStudent.Application.Tests;

public class CreateRelationHandlerTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IChatRepository> _chatRepoMock;
    private readonly Mock<ILogger<CreateMentorStudentRelationCommandHandler>> _loggerMock;
    private readonly CreateMentorStudentRelationCommandHandler _handler;

    public CreateRelationHandlerTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _chatRepoMock = new Mock<IChatRepository>();

        _loggerMock = new Mock<ILogger<CreateMentorStudentRelationCommandHandler>>();
        _handler = new CreateMentorStudentRelationCommandHandler(
            _userRepoMock.Object,
            _chatRepoMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenMentorIsNotMentorRole()
    {
        var studentid = Guid.NewGuid();
        var fakeMentorId = Guid.NewGuid();

        var student = new User(studentid, new Domain.ValueObjects.Email("student@gmail.com"), "test", Domain.Enums.UserRole.Student);
        var fakeMentor = new User(fakeMentorId, new Domain.ValueObjects.Email("fakementor@gmail.com"), "test", Domain.Enums.UserRole.Student);

        _userRepoMock.Setup(x => x.GetByIdAsync(fakeMentorId)).ReturnsAsync(fakeMentor);
        _userRepoMock.Setup(x => x.GetByIdAsync(studentid)).ReturnsAsync(student);

        var command = new CreateMentorStudentRelationCommand(studentid, fakeMentorId);
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()  
            .WithMessage("A felhasználó nem mentor!");
    }
    [Fact]
    public async Task Handle_ShouldCreateChat_WhenEverythingIsValid()
    {
        var studentId = Guid.NewGuid();
        var mentorId = Guid.NewGuid();

        var mentorUser = new User(mentorId, new Email("m@t.com"), "pw", UserRole.Mentor);
        var studentUser = new User(studentId, new Email("s@t.com"), "pw", UserRole.Student);

        _userRepoMock.Setup(x => x.GetByIdAsync(mentorId)).ReturnsAsync(mentorUser);
        _userRepoMock.Setup(x => x.GetByIdAsync(studentId)).ReturnsAsync(studentUser);

        var command = new CreateMentorStudentRelationCommand(studentId, mentorId);

        var result = await _handler.Handle(command,CancellationToken.None);

        _chatRepoMock.Verify(x => x.AddAsync(It.IsAny<Chat>()), Times.Once);

        result.Should().NotBeEmpty();

    }
}
