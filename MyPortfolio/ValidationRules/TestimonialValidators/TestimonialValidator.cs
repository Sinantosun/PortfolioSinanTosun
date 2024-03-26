

using FluentValidation;
using MyPortfolio.Models;

namespace MyPortfolio.ValidationRules.TestimonialValidators
{
    public class TestimonialValidator:AbstractValidator<Testimonials>
    {
        public TestimonialValidator()
        {
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.NameSurname).MaximumLength(50).WithMessage("Bu maximumu 50 karakter içermelidir.");
            RuleFor(x => x.Comment).MaximumLength(500).WithMessage("Bu alan maxiumum 500 karakter içermelidir.");
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alanı doldurun.");
            RuleFor(x => x.Title).MaximumLength(30).WithMessage("Bu alan maximumu 30 karakter içermelidir.");
        }
    }
}