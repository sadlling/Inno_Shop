using FluentValidation;
using UserManagement.Application.Features.AuthenticateFeatures.ForgotPassword;

namespace UserManagement.Application.Validators
{
    public class ForgotPasswordValidator:AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordValidator() 
        {
            RuleFor(request=>request.email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
