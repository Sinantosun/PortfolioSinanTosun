

using FluentValidation;
using MyPortfolio.Models;

namespace MyPortfolio.ValidationRules.TeamsValidatiors
{
    public class TeamsValidator:AbstractValidator<Teams>
    {
        public TeamsValidator()
        {
            RuleFor(x => x.ImageURL).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.Açıklama).NotEmpty().WithMessage("Bu alanı doldurun.");

        }
    }
}