using FluentValidation;
using UserManagement.Application.Features.UserFeatures;

namespace UserManagement.Application.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(user => user.UserName).
               NotEmpty()
               .MinimumLength(5)
               .MaximumLength(12);
            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(user => user.Password)
                .Matches(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,16}$")
                .WithMessage("Password must be contain one number, one uppercase letters,1 lowercase letters,1 non-alpha numeric number,8-16 characters with no space");
            RuleFor(user => user.PhoneNumber)
                .Matches(@"^\+375 \((17|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$")
                .WithMessage("Phone number must be look like: +375(17|29|33|44) 111-11-11");
        }
    }
}
