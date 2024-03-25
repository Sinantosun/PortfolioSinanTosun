using FluentValidation;
using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolio.ValidationRules.CategoryValidators
{
    public class CategoryValidator:AbstractValidator<Categories>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Bu alan maximum 50 karakter içermelidir.");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Bu alan minimum 5 karakter içermelidir.");
        }
    }
}