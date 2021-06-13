using Common.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Common.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.AccountType).IsInEnum().WithMessage(Resources.DataModelValidationErrorMessages.InvalidAccountType);
            RuleFor(x => x.Password).Equal(x => x.RepeatedPassword).WithMessage(Resources.DataModelValidationErrorMessages.PasswordMismatch);
            RuleFor(x => x.Username).Must(CheckUsername).WithMessage(Resources.DataModelValidationErrorMessages.SpecialCharactersUsername);
        }
        
        private bool CheckUsername(string username)
        {
            var regex = new Regex(@"[""!@$%^&*(){}:;<>,.?/+_=\|'~\\]");
            return !regex.IsMatch(username);
        }
    }
}
