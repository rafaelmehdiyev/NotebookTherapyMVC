
namespace BusinessLogicLayer.Utilities.Validations.FluentValidation
{
    public class ProductPostDtoValidator : AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Don't Enter Empty ")
            .NotNull()
             .WithMessage("Don't Enter Null ")
               .MinimumLength(2).
                MaximumLength(255)
                .WithMessage("Enter valid Name")
            .Must(ValidName);

        }
        private bool ValidName(string name)
        {
            if (name is null)
            {
                return false;
            }
            //var nameRegex = @"^[A-Z]+[a-zA-Z]*$|[A-Z]+[a-zA-Z]+[\s][A-Z]+[a-zA-Z]*$";
            string nameRegex = @"^[A-Z]+[a-zA-Z]$|[A-Z]+[a-zA-Z]+[\s][A-Z]+[a-zA-Z]|[A-Za-z0-9☾]+$";
            Regex regex = new(nameRegex);

            if (regex.IsMatch(name))
            {
                return true;
            }
            return false;
        }
    }



}

