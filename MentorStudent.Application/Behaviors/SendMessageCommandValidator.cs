using FluentValidation;
using MentorStudent.Application.Commands;

namespace MentorStudent.Application.Behaviors
{
    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator()
        {
            RuleFor(x => x.ChatId)
                .NotEmpty().WithMessage("A Chat azonosító hiányzik.");

            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("A küldő azonosítója hiányzik.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Az üzenet nem lehet üres.")
                .MaximumLength(2000).WithMessage("Az üzenet túl hosszú (maximum 2000 karakter).");
        }
    }
}