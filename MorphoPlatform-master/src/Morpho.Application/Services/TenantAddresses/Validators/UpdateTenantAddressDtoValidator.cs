using FluentValidation;
using Morpho.Common.Validators;
using Morpho.Domain.Enums;
using Morpho.Services.TenantAddresses.Dtos;
using System;

namespace Morpho.Services.TenantAddresses.Validators
{
    public class UpdateTenantAddressDtoValidator : AbstractValidator<UpdateTenantAddressDto>
    {
        public UpdateTenantAddressDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.TenantId)
                .NotNull().WithMessage("TenantId is required.")
                .GreaterThan(0).WithMessage("TenantId must be greater than 0.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .Must(BeValidAddressType).WithMessage("Invalid address type.");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("Address is required.")
                .SetValidator(new AddressValidator());
        }

        private bool BeValidAddressType(string type)
        {
            return Enum.TryParse(typeof(TenantAddressType), type, true, out _);
        }
    }

}
