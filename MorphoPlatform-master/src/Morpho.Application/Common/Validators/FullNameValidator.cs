using FluentValidation;
using Morpho.Domain.Common;

namespace Morpho.Common.Validators
{
    public class FullNameValidator : AbstractValidator<FullName>
    {
        public FullNameValidator()
        {
            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage("English name is required")
                .MaximumLength(128);

            RuleFor(x => x.NameAr)
                .MaximumLength(128);
        }
    }
}
