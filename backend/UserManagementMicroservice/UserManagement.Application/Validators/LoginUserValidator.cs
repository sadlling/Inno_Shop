using FluentValidation;
using UserManagement.Application.Features.AuthenticateFeatures.Login;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Validators
{
    public class LoginUserValidator: AbstractValidator<LoginRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(user => user.UserName).
               NotEmpty()
               .MinimumLength(5)
               .MaximumLength(12);
            RuleFor(user => user.Password)
                .Matches(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,16}$")
                .WithMessage("Password must be contain one number, one uppercase letters,1 lowercase letters,1 non-alpha numeric number,8-16 characters with no space");
        }
    }
}
