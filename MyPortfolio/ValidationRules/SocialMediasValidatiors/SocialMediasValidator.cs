using FluentValidation;
using MyPortfolio.Models;


namespace MyPortfolio.ValidationRules.SocialMediasValidatiors
{
    public class SocialMediasValidator:AbstractValidator<SocialMedias>
    {
        public SocialMediasValidator()
        {
            RuleFor(x => x.Icon).NotEmpty().WithMessage("Bu alanı doldurun");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alanı doldurun");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Bu maximum 50 karakter içermelidir.");
            RuleFor(x => x.URL).NotEmpty().WithMessage("Bu alanı doldurun");
        }
    }
}