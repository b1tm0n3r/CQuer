using Common.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Common.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.AccountType).IsInEnum().WithMessage("Invalid Account Type!");
            RuleFor(x => x.Password).Equal(x => x.RepeatedPassword).WithMessage("Passwords are not the same!");
            RuleFor(x => x.Username).Must(CheckUsername).WithMessage("Username cannot contain special characters!");
        }
        
        private bool CheckUsername(string username)
        {
            var regex = new Regex(@"[""!@$%^&*(){}:;<>,.?/+_=\|'~\\]");
            return !regex.IsMatch(username);
        }
    }
}
