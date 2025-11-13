using FluentValidation;
using Morpho.Domain.Enums;
using System;
using Morpho.Services.TenantDocuments.Dtos;

namespace Morpho.Services.TenantDocuments.Validators
{
    public class UpdateTenantDocumentDtoValidator : AbstractValidator<UpdateTenantDocumentDto>
    {
        public UpdateTenantDocumentDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.TenantId)
                .NotNull().WithMessage("TenantId is required.")
                .GreaterThan(0).WithMessage("TenantId must be greater than zero.");

            RuleFor(x => x.DocType)
                .NotEmpty().WithMessage("DocType is required.")
                .Must(BeValidDocType).WithMessage("Invalid document type.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(256);

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.StorageKey)
                .NotEmpty().WithMessage("StorageKey is required.")
                .MaximumLength(512);

            RuleFor(x => x.FileHash)
                .MaximumLength(128)
                .When(x => !string.IsNullOrEmpty(x.FileHash));

            RuleFor(x => x.IssuedBy)
                .MaximumLength(256)
                .When(x => !string.IsNullOrEmpty(x.IssuedBy));

            RuleFor(x => x.Version)
                .MaximumLength(64)
                .When(x => !string.IsNullOrEmpty(x.Version));

            RuleFor(x => x.ExpiresAt)
                .GreaterThan(x => x.IssuedAt)
                .When(x => x.IssuedAt.HasValue && x.ExpiresAt.HasValue)
                .WithMessage("ExpiresAt must be later than IssuedAt.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(BeValidStatus).WithMessage("Invalid status.");

            RuleFor(x => x.MimeType)
                .Must(BeValidMimeType).WithMessage("Invalid MIME type.")
                .When(x => !string.IsNullOrEmpty(x.MimeType));
        }

        private bool BeValidDocType(string docType)
        {
            return Enum.TryParse(typeof(DocumentType), docType, true, out _);
        }

        private bool BeValidStatus(string status)
        {
            return Enum.TryParse(typeof(DocumentStatus), status, true, out _);
        }

        private bool BeValidMimeType(string? mimeType)
        {
            if (string.IsNullOrEmpty(mimeType)) return true;
            return Enum.TryParse(typeof(MimeType), mimeType, true, out _);
        }
    }

}
