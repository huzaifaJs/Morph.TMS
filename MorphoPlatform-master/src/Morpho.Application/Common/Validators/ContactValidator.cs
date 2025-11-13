using FluentValidation;
using Morpho.Domain.Common;

namespace Morpho.Common.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .MaximumLength(256);

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName is required.")
                .MaximumLength(128);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(256);

            RuleFor(x => x.Phone)
                .MaximumLength(32);

            RuleFor(x => x.IsPrimary)
                .NotNull().WithMessage("IsPrimary flag must be set.");
        }
    }

}
