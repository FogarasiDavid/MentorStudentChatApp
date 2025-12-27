using FluentValidation;
using MentorStudent.Application.Commands;

namespace MentorStudent.Application.Behaviors
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Az email cím nem lehet üres.")
                .EmailAddress().WithMessage("Helytelen email formátum.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A jelszó nem lehet üres.");
        }
    }
}