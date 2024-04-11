using FluentValidation;
using UserManagement.Application.Features.UserFeatures.UpdateUser;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Validators
{
    public class UpdateUserValidator:AbstractValidator<UpdateUserRequest>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(user => user.UserName).
               NotEmpty()
               .MinimumLength(5)
               .MaximumLength(12)
               .MustAsync(IsUniqueUsername).WithMessage("Username already exist");
            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(IsUniqueEmail).WithMessage("Email already exist");
            RuleFor(user => user.PhoneNumber)
                .Matches(@"^\+375 \((17|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$")
                .WithMessage("Phone number must be look like: +375(17|29|33|44) 111-11-11");
        }
        private async Task<bool> IsUniqueUsername(string username, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return user == null;
        }
        private async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user == null;
        }
    }
}
