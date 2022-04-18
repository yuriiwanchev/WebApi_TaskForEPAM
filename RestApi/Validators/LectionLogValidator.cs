using Domain.Models;
using FluentValidation;

namespace RestApi.Validators
{
    public class LectionLogValidator : AbstractValidator<LectionLog>
    {
        public LectionLogValidator()
        {
            RuleFor(x => x)
                .Must(LectionPass).WithMessage("If a student missed a lecture, then he must get 0 score");
            // RuleFor(x => x.Attendance)
            //     .NotEmpty();
            RuleFor(x => x.Score)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(5);
        }

        private bool LectionPass(LectionLog log)
        {
            if (!log.Attendance)
            {
                if (log.Score != 0) return false;
            }

            return true;
        }
    }
}