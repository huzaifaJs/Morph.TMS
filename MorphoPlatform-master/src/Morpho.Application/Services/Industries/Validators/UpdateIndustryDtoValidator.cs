using FluentValidation;
using Morpho.Services.Industries.Dtos;

namespace Morpho.Services.Industries.Validators
{
    public class UpdateIndustryDtoValidator : AbstractValidator<UpdateIndustryDto>
    {
        public UpdateIndustryDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(128);
        }
    }
}
