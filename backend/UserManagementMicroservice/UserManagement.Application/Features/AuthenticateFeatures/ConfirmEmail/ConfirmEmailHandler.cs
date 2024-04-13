using MediatR;
using UserManagement.Application.Common.CustomExceptions;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Features.AuthenticateFeatures.ConfirmEmail
{
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailRequest, Unit>
    {
        private readonly IUserRepository _userRepository;
        public ConfirmEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(request.userEmail);
            if (user == null)
            {
                throw new NotFoundException($"User with email {request.userEmail} not found");
            }
            await _userRepository.ConfirmEmailAsync(user, request.token);

            return Unit.Value;
        }
    }
}
