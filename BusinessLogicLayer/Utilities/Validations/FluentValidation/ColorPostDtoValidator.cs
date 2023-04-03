using System.Text.RegularExpressions;

namespace BusinessLogicLayer.Utilities.Validations.FluentValidation { 

public class ColorPostDtoValidator : AbstractValidator<ColorPostDto>
    {
        public ColorPostDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Don't Enter Empty ")
            .NotNull()
             .WithMessage("Don't Enter Null ")
               .MinimumLength(2).
                MaximumLength(255)
                .WithMessage("Enter valid Color")
            .Must(ValidName);

        }
        private bool ValidName(string name)
        {
            var nameRegex = "^[a-zA-Z]+$";
            Regex regex = new Regex(nameRegex);
            if (regex.IsMatch(name))
            {
                return true;
            }
            return false;
        }
    }
    


}