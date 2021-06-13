using Common.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Common.Validators
{
    public class TicketDtoValidator : AbstractValidator<TicketDto>
    {
        public TicketDtoValidator()
        {
            RuleFor(x => x.DownloadUrl).NotNull().WithMessage(Resources.DataModelValidationErrorMessages.NullUrl)
                .NotEmpty().WithMessage(Resources.DataModelValidationErrorMessages.EmptyUrl)
                .Must(CheckUrlBadChars).WithMessage(Resources.DataModelValidationErrorMessages.SpecialCharactersUrl);

            RuleFor(x => x.Severity).GreaterThanOrEqualTo(1).WithMessage(Resources.DataModelValidationErrorMessages.SeverityLessThanMin)
                .LessThanOrEqualTo(5).WithMessage(Resources.DataModelValidationErrorMessages.SeverityGreaterThanMax);

            RuleFor(x => x.Description).Must(CheckDescriptionBadChars).WithMessage(Resources.DataModelValidationErrorMessages.SpecialCharactersTicketDescription);
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
