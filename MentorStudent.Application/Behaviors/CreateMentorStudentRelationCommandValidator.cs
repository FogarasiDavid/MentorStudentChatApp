using FluentValidation;
using MentorStudent.Application.Commands;

namespace MentorStudent.Application.Behaviors
{
    public class CreateMentorStudentRelationCommandValidator : AbstractValidator<CreateMentorStudentRelationCommand>
    {
        public CreateMentorStudentRelationCommandValidator()
        {
            RuleFor(x => x.studentId)
                .NotEmpty().WithMessage("A tanuló azonosítója kötelező.")
                .NotEqual(Guid.Empty);

            RuleFor(x => x.mentorId)
                .NotEmpty().WithMessage("A mentor azonosítója kötelező.")
                .NotEqual(Guid.Empty);

            RuleFor(x => x)
                .Must(x => x.studentId != x.mentorId)
                .WithMessage("A mentor és a diák nem lehet ugyanaz a személy.");
        }
    }
}