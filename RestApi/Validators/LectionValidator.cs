using Domain.Models;
using FluentValidation;

namespace RestApi.Validators
{
    public class LectionValidator : AbstractValidator<Lection>
    {
        public LectionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .Matches(@"\p{L}").WithMessage("'{PropertyName}' must contain one or more letter.")
                .Matches("[A-ZА-Я]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
                .Matches("[a-zа-я]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.");

            // RuleFor(x => x.Date).
        }
    }
}