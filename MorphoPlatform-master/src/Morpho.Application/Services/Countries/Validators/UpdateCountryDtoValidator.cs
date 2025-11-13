using FluentValidation;
using Morpho.Common.Validators;
using Morpho.Services.Countries.Dtos;

namespace Morpho.Services.Countries.Validators
{
    public class UpdateCountryDtoValidator : AbstractValidator<UpdateCountryDto>
    {
        public UpdateCountryDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.")
                .SetValidator(new FullNameValidator());

            RuleFor(x => x.PhoneCode)
                .MaximumLength(8);
        }
    }
}
