

using FluentValidation;
using MyPortfolio.Models;

namespace MyPortfolio.ValidationRules.SkillsValidators
{
    public class SkillValidator:AbstractValidator<Skills>
    {
        public SkillValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Bu alanı doldurun");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alanı doldurun");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Bu alan maximum 50 karakter içeremelidir.");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Bu alan minimum 5 karakter içeremelidir.");
            RuleFor(x => x.Amount).LessThanOrEqualTo((byte)100).WithMessage("Girilen % oranı 100 den kücük veya eşit olamalıdır.");
        }
    }
}