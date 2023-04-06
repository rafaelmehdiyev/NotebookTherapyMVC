
namespace BusinessLogicLayer.Utilities.Validations.FluentValidation
{
    public class FAQCategoryPostDtoValidator : AbstractValidator<FAQCategoryPostDto>
    {
        public FAQCategoryPostDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Don't Enter Empty ")
            .NotNull()
             .WithMessage("Don't Enter Null ")
               .MinimumLength(2).
                MaximumLength(255)
                .WithMessage("Enter valid FAQCategory")
            .Must(ValidName);

        }
        private bool ValidName(string name)
        {
            if (name is null)
            {
                return false;
            }
            var nameRegex = @"^[A-Z]+[a-zA-Z]*$|[A-Z]+[a-zA-Z]+[\s][A-Z]+[a-zA-Z]*$";
            Regex regex = new(nameRegex);

            if (regex.IsMatch(name))
            {
                return true;
            }
            return false;
        }


    }




}

