

using FluentValidation;
using MyPortfolio.Models;

namespace MyPortfolio.ValidationRules.ServicesValidatiors
{
    public class ServicesValidator:AbstractValidator<Services>
    {
        public ServicesValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alanı Doldurun.");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Bu alan maximum 50 karakter içermelidir.");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Bu alan minimum 5 karakter içermelidir.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Bu alanı Doldurun.");
            RuleFor(x => x.Description).MaximumLength(50).WithMessage("Bu alan maximum 50 karakter içermelidir.");
            RuleFor(x => x.Description).MinimumLength(5).WithMessage("Bu alan minimum 5 karakter içermelidir.");
            RuleFor(x => x.Icon).NotEmpty().WithMessage("Bu alanı Doldurun.");
            
        }
    }
}