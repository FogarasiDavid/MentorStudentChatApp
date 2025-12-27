using FluentAssertions;
using MentorStudent.Domain.Entities;

namespace MentorStudent.Domain.Tests;

public class UserTests
{
    [Fact]
    public void AttachMentorRole_ShouldThrowException_WhenRoleIsNotMentor()
    {
        var email = new ValueObjects.Email("diak@gmail.com");
        var User = new User(Guid.NewGuid(), email,"hash", Enums.UserRole.Student);
        var profile = new MentorProfile(User.Id, "Bio", "C#");

        Action act = () => User.AttachMentorRole(profile);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("A felhasználó nem mentor");

    }
    [Fact]
    public void AttachMentorRole_ShouldSucceed_WhenRoleIsNotMentor()
    {
        var email = new ValueObjects.Email("diak@gmail.com");
        var User = new User(Guid.NewGuid(), email, "hash", Enums.UserRole.Mentor);
        var profile = new MentorProfile(User.Id, "Bio", "C#");

        Action act = () => User.AttachMentorRole(profile);

        act.Should().NotThrow();

        User.MentorProfile.Should().NotBeNull();
    }
}
