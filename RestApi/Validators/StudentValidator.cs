using System.Linq;
using Domain.Models;
using FluentValidation;

namespace RestApi.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(40)
                .Matches(@"\p{L}").WithMessage("'{PropertyName}' must contain one or more letter.")
                .Matches("[A-ZА-Я]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
                .Matches("[a-zа-я]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.")
                .Matches(@"^[^£#“”!@$%^&*(){}:;<>,.?/+=|~\\]*$").WithMessage("'{PropertyName}' must not contain one or more special characters.");
            RuleFor(x => x.PhoneNumber)
                .Must(PhoneNumberCheck).WithMessage("'{PropertyName}' must be format like +X (XXX) XXX-XX-XX or X XXX XXX-XX-XX or +XXX (XX) XXX-XXXX.");
            
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("'{PropertyName}' not correct.");
        }
        
        private bool PhoneNumberCheck(string str)
        {
            var parts = str.Split(' ');
            if (parts[0][0] == '+') parts[0] = parts[0].Trim('+');
            if (ConsistOfDigits(parts[0]))
            {
                if ((parts[1].First() == '(' && parts[1].Last() == ')')) parts[1] = parts[1].Trim('(', ')');
                if (ConsistOfDigits(parts[1]))
                {
                    var end = parts[2].Split('-');
                    foreach (var part in end)
                    {
                        if(!ConsistOfDigits(part)) return false;
                    }

                    return true;
                }
            }

            return false;
        }

        private bool ConsistOfDigits(string str)
        {
            foreach (var c in str)
            {
                if (!char.IsDigit(c)) return false;
            }

            return true;
        }
    }
}