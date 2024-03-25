using FluentValidation;
using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.ValidationRules.AboutValidators
{
    public class AboutValidator : AbstractValidator<Abouts>
    {
        public AboutValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(x => x.Description).MaximumLength(250).WithMessage("Bu alan maximum 250 karakter içermelidir.");
            RuleFor(x => x.Description).MinimumLength(10).WithMessage("Bu alan minimum 10 karakter içermelidir.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Bu alan minimum 5 karakter içermelidir.");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Bu alan maximum 50 karakter içermelidir.");
        
        }
    }
}