using FluentValidation;
using UserManagement.Application.Features.AuthenticateFeatures.ResetPassword;

namespace UserManagement.Application.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(request => request.email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(request => request.password)
                .Matches(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,16}$")
                .WithMessage("Password must be contain one number, one uppercase letters,1 lowercase letters,1 non-alpha numeric number,8-16 characters with no space");
            RuleFor(request => request.confirmPassword)
                .Equal(request=>request.password).WithMessage("Password and confirm password must be match")
                .NotEmpty();
                
        }
    }
}
