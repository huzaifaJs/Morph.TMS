using FluentValidation;
using Morpho.Services.CompanyTypes.Dtos;
using Morpho.Services.Industries.Dtos;

namespace Morpho.Services.Industries.Validators
{
    public class CreateIndustryDtoValidator : AbstractValidator<CreateIndustryDto>
    {
        public CreateIndustryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(128);
        }
    }
}
