using FluentValidation;
using MyPortfolio.Models;

namespace MyPortfolio.ValidationRules.FeatureValidators
{
    public class FeatureValidator : AbstractValidator<Features>
    {
        public FeatureValidator()
        {


            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(x => x.NameSurname).MaximumLength(50).WithMessage("Bu alan maximum 25 karakter içermelidir.");
            RuleFor(x => x.NameSurname).MinimumLength(5).WithMessage("Bu alan maximum 5 karakter içermelidir.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(x => x.Title).MinimumLength(3).WithMessage("Bu alan minumum 3 karakter içermelidir.");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Bu alan maximum 15 karakter içermelidir.");


        }
    }
}