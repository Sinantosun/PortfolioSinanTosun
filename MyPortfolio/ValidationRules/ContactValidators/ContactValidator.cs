
using FluentValidation;
using MyPortfolio.Models;

namespace MyPortfolio.ValidationRules.ContactValidators
{
    public class ContactValidator : AbstractValidator<Contacts>
    {
        public ContactValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(x => x.NameSurname).MaximumLength(50).WithMessage("Bu alan maxiumum 50 karakter içermelidir.");
            RuleFor(x => x.NameSurname).MinimumLength(5).WithMessage("Bu alan minimum 5 karakter içermelidir.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(x => x.Address).MaximumLength(250).WithMessage("Bu alan maximum 250 karakter içermelidir.");
            RuleFor(x => x.Address).MinimumLength(5).WithMessage("Bu alan maximum 5 karakter içermelidir.");
        }
    }
}