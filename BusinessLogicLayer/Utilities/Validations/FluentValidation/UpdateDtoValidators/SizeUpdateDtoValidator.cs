
namespace BusinessLogicLayer.Utilities.Validations.FluentValidation.UpdateDtoValidators;


    public class SizeUpdateDtoValidator : AbstractValidator<SizeUpdateDto>
    {
        public SizeUpdateDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
               .WithMessage("Don't Enter Empty ")
            .NotNull()
             .WithMessage("Don't Enter Null ")
               .MaximumLength(255)
 .Must(ValidName).
        WithMessage(Messages.EnterValid(Messages.Size));

    }
        private bool ValidName(string name)
        {
            if (name is not null)
            {
                string nameRegex = @"^[a-zA-Z0-9\s()]*$";
                Regex regex = new(nameRegex);

                if (regex.IsMatch(name))
                {
                    return true;
                }
            }         
            return false;
        }
    }

