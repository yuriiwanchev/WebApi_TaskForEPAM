using Domain.Models;
using FluentValidation;

namespace RestApi.Validators
{
    public class HomeworkValidator : AbstractValidator<Homework>
    {
        public HomeworkValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(10000)
                .Matches(@"\p{L}").WithMessage("'{PropertyName}' must contain one or more letter.");
        }
    }
}