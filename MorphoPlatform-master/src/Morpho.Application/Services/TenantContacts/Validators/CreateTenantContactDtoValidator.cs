using FluentValidation;
using Morpho.Common.Validators;
using Morpho.Services.TenantContacts.Dtos;

namespace Morpho.Services.TenantContacts.Validators
{
    public class CreateTenantContactDtoValidator : AbstractValidator<CreateTenantContactDto>
    {
        public CreateTenantContactDtoValidator()
        {
            RuleFor(x => x.TenantId)
                .NotNull().WithMessage("TenantId is required.")
                .GreaterThan(0).WithMessage("TenantId is required.");

            RuleFor(x => x.Contact)
                .NotNull().WithMessage("Contact is required.")
                .SetValidator(new ContactValidator());
        }
    }
}
