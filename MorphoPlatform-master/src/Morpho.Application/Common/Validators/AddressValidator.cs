using FluentValidation;
using Morpho.Domain.Common;

namespace Morpho.Common.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.AddressLine1)
                .NotEmpty().WithMessage("Address Line 1 is required.")
                .MaximumLength(256);

            RuleFor(x => x.AddressLine2)
                .MaximumLength(256);

            RuleFor(x => x.City)
                .MaximumLength(128);

            RuleFor(x => x.StateProvince)
                .MaximumLength(128);

            RuleFor(x => x.PostalCode)
                .MaximumLength(32);

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage("Country code is required.")
                .Length(2).WithMessage("Country code must be 2 characters.");

            RuleFor(x => x.IsPrimary)
                .NotNull().WithMessage("IsPrimary must be specified.");
        }
    }
}
