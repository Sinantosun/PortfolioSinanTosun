using FluentValidation;
using MyPortfolio.Models;

namespace MyPortfolio.ValidationRules.ProjectValidators
{
    public class ProjectValidator : AbstractValidator<Projects>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.Category).NotEmpty().WithMessage("Seçim yapmadınız.");
            RuleFor(x => x.Github).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Bu alana maximum 50 karakter girilmelidir.");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Bu alana minumum 3 karakter girilmelidir.");
        }
    }
}