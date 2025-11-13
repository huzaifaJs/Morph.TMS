using FluentValidation;
using Morpho.Common.Validators;
using Morpho.Domain.Common;
using Morpho.Services.Countries.Dtos;

namespace Morpho.Services.Countries.Validators
{
    public class CreateCountryDtoValidator : AbstractValidator<CreateCountryDto>
    {
        public CreateCountryDtoValidator()
        {
            RuleFor(x => x.Code)
                .Length(2).WithMessage("Country code must be 2 characters");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.")
                .SetValidator(new FullNameValidator());

            RuleFor(x => x.PhoneCode)
                .MaximumLength(8);
        }
    }
}
