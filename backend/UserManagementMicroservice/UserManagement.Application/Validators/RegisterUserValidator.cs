using FluentValidation;
using UserManagement.Application.Features.AuthenticateFeatures.Register;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterUserValidator(IUserRepository userRepository)
        {
            RuleFor(req=>req.user).SetValidator(new CreateUserValidator(userRepository));
        }
    }
}
