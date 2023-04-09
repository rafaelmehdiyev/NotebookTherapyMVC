namespace BusinessLogicLayer.Utilities.Validations.FluentValidation.UpdateDtoValidators
{
    public class FAQUpdateDtoValidator : AbstractValidator<FAQUpdateDto>
    {
        public FAQUpdateDtoValidator()
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
