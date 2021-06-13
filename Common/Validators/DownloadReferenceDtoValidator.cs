using Common.DTOs;
using FluentValidation;

namespace Common.Validators
{
    public class DownloadReferenceDtoValidator : AbstractValidator<DownloadReferenceDto>
    {
        public DownloadReferenceDtoValidator()
        {
            RuleFor(x => x.TicketId).NotNull().GreaterThan(0).WithMessage(Resources.DataModelValidationErrorMessages.MissingTicketId);
            RuleFor(x => x.DownloadUrl).NotEmpty().WithMessage(Resources.DataModelValidationErrorMessages.EmptyUrl);
        }
    }
}
