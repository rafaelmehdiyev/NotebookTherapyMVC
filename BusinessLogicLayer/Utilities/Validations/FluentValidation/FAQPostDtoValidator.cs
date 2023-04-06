
namespace BusinessLogicLayer.Utilities.Validations.FluentValidation
{
    public class FAQPostDtoValidator : AbstractValidator<FAQPostDto>
    {
        public FAQPostDtoValidator()
        {
            RuleFor(c => c.Question)
                .NotEmpty()
                .WithMessage("Don't Enter Empty ")
            .NotNull()
             .WithMessage("Don't Enter Null ")
               .MinimumLength(2).
                MaximumLength(255)
                .WithMessage("Enter valid Question");
            RuleFor(c => c.Answer)
                  .NotEmpty()
                .WithMessage("Don't Enter Empty ")
            .NotNull()
             .WithMessage("Don't Enter Null ")
               .MinimumLength(2).
                MaximumLength(255)
                .WithMessage("Enter valid Answer");
        }
    }
}
