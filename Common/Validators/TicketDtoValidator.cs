using Common.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Common.Validators
{
    public class TicketDtoValidator : AbstractValidator<TicketDto>
    {
        public TicketDtoValidator()
        {
            RuleFor(x => x.DownloadUrl).NotNull().WithMessage("Download URL cannot be null")
                .NotEmpty().WithMessage("Download URL cannot be empty")
                .Must(CheckUrlBadChars).WithMessage("Special characters not allowed in URL");

            RuleFor(x => x.Severity).GreaterThanOrEqualTo(1).WithMessage("Severity minimum value is 1")
                .LessThanOrEqualTo(5).WithMessage("Severity maximum value is 5");

            RuleFor(x => x.Description).Must(CheckDescriptionBadChars).WithMessage("Special characters not allowed in description");
        }
        private bool CheckDescriptionBadChars(string description)
        {
            var regex = new Regex(@"[\^{}$<>;_=\|~\\]");
            return !regex.IsMatch(description);
        }
        private bool CheckUrlBadChars(string url)
        {
            var regex = new Regex(@"[\^<>;_\|]");
            return !regex.IsMatch(url);
        }
    }
}
