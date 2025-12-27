using FluentValidation;
using MentorStudent.Application.Queries;

namespace MentorStudent.Application.Behaviors
{
    public class GetMessagesQueryValidator : AbstractValidator<GetMessagesQuery>
    {
        public GetMessagesQueryValidator()
        {
            RuleFor(x => x.ChatId)
                .NotEmpty().WithMessage("A Chat azonosító nem lehet üres.");

            RuleFor(x => x.RequestingUserId)
                .NotEmpty().WithMessage("A lekérdező felhasználó azonosítója nem lehet üres.");
        }
    }
}