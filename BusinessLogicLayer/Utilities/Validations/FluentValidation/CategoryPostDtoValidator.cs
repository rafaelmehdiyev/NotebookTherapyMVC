namespace BusinessLogicLayer.Utilities.Validations.FluentValidation;

public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
{
    public CategoryPostDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Don't Enter Empty ")
        .NotNull()
         .WithMessage("Don't Enter Null ")
           .MinimumLength(2).
            MaximumLength(255)
            .WithMessage("Enter valid Category")
        .Must(ValidName);

    }
    private bool ValidName(string name)
    {
        string nameRegex = "^[a-zA-Z]+$";
        Regex regex = new(nameRegex);
        if (regex.IsMatch(name))
        {
            return true;
        }
        return false;
    }
}
