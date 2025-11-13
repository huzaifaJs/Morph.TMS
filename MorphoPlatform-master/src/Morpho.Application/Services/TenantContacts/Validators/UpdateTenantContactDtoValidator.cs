using FluentValidation;
using Morpho.Common.Validators;
using Morpho.Services.TenantContacts.Dtos;

namespace Morpho.Services.TenantContacts.Validators
{
    public class UpdateTenantContactDtoValidator : AbstractValidator<UpdateTenantContactDto>
    {
        public UpdateTenantContactDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id is required.");

            RuleFor(x => x.TenantId)
                .NotNull().WithMessage("TenantId is required.")
                .GreaterThan(0).WithMessage("TenantId is required.");

            RuleFor(x => x.Contact)
                .NotNull().WithMessage("Contact is required.")
                .SetValidator(new ContactValidator());
        }
    }

}
