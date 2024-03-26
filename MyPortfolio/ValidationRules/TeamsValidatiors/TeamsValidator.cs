

using FluentValidation;
using MyPortfolio.Models;

namespace MyPortfolio.ValidationRules.TeamsValidatiors
{
    public class TeamsValidator:AbstractValidator<Teams>
    {
        public TeamsValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.Description).MaximumLength(150).WithMessage("Bu alanı maxiumum 150 karakter içermelidir.");
        }
    }
}