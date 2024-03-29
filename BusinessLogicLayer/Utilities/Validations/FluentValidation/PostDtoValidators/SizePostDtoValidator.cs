﻿
namespace BusinessLogicLayer.Utilities.Validations.FluentValidation.PostDtoValidators;


    public class SizePostDtoValidator : AbstractValidator<SizePostDto>
    {
        public SizePostDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
               .WithMessage("Don't Enter Empty ")
            .NotNull()
             .WithMessage("Don't Enter Null ")
               .MaximumLength(255)
                .WithMessage("Enter valid Size")
            .Must(ValidName);

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

