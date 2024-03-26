using FluentValidation;
using MyPortfolio.Models;

namespace MyPortfolio.ValidationRules.MessagesValidators
{
    public class MessageValidator:AbstractValidator<Messages>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("bu alanı doldurunuz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("geçerli bir email adresi giriniz.");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("bu alanı doldurunuz.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("bu alanı doldurunuz.");
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("bu alanı doldurunuz.");
        }
    }
}