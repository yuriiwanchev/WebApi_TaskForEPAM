using Domain.Models;
using FluentValidation;

namespace RestApi.Validators
{
    public class LectorValidator : AbstractValidator<Lector>
    {
        public LectorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(40)
                .Matches(@"\p{L}").WithMessage("'{PropertyName}' must contain one or more letter.")
                .Matches("[A-ZА-Я]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
                .Matches("[a-zа-я]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.")
                .Matches(@"^[^£#“”!@$%^&*(){}:;<>,.?/+=|~\\]*$").WithMessage("'{PropertyName}' must not contain one or more special characters.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("'{PropertyName}' not correct.");
        }
    }
}