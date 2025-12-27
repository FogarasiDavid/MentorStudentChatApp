using FluentValidation;
using MentorStudent.Application.Commands;

namespace MentorStudent.Application.Behaviors
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Az email cím megadása kötelező.")
                .EmailAddress().WithMessage("Érvénytelen email formátum.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A jelszó nem lehet üres.")
                .MinimumLength(3).WithMessage("A jelszónak legalább 3 karakter hosszúnak kell lennie.");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Érvénytelen szerepkör.");
        }
    }
}