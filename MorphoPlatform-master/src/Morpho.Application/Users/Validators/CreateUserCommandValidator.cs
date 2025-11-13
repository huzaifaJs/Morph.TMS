using FluentValidation;
using Morpho.Application.Commands.Users;

namespace Morpho.Users.Validators
{
    /// <summary>
    /// Validator for CreateUserCommand.
    /// </summary>
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(64)
                .WithMessage("Name cannot exceed 64 characters");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Surname is required")
                .MaximumLength(64)
                .WithMessage("Surname cannot exceed 64 characters");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required")
                .MaximumLength(256)
                .WithMessage("Username cannot exceed 256 characters")
                .Matches("^[a-zA-Z0-9_.-]+$")
                .WithMessage("Username can only contain letters, numbers, dots, hyphens and underscores");

            RuleFor(x => x.EmailAddress)
                .NotNull()
                .WithMessage("Email address is required");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$")
                .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, and one digit");

            RuleFor(x => x.RoleNames)
                .NotNull()
                .WithMessage("Role names cannot be null");
        }
    }
}