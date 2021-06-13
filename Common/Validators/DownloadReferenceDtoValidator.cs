using Common.DTOs;
using FluentValidation;

namespace Common.Validators
{
    public class DownloadReferenceDtoValidator : AbstractValidator<DownloadReferenceDto>
    {
        public DownloadReferenceDtoValidator()
        {
            RuleFor(x => x.TicketId).NotNull().GreaterThan(0).WithMessage("Ticket ID required!");
            RuleFor(x => x.DownloadUrl).NotEmpty().WithMessage("Download URL cannot be empty!");
        }
    }
}
