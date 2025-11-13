using FluentValidation;
using Morpho.Services.CompanyTypes.Dtos;

namespace Morpho.Services.CompanyTypes.Validators
{
    public class UpdateCompanyTypeDtoValidator : AbstractValidator<UpdateCompanyTypeDto>
    {
        public UpdateCompanyTypeDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(128);
        }
    }
}
