﻿
namespace BusinessLogicLayer.Utilities.Validations.FluentValidation.UpdateDtoValidators;

public class ProductCollectionUpdateDtoValidator : AbstractValidator<ProductCollectionUpdateDto>
{
    public ProductCollectionUpdateDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Don't Enter Empty ")
        .NotNull()
         .WithMessage("Don't Enter Null ")
           .MinimumLength(2).
            MaximumLength(255)
                      .Must(ValidName).
        WithMessage(Messages.EnterValid(Messages.ProductCollection));

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


    


