using Abp.Domain.Repositories;
using FluentValidation;
using Morpho.Domain.Entities;
using Morpho.Services.CompanyTypes.Dtos;

namespace Morpho.Services.CompanyTypes.Validators
{
    public class CreateCompanyTypeDtoValidator : AbstractValidator<CreateCompanyTypeDto>
    {
        public CreateCompanyTypeDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(128);
        }
    }
}
