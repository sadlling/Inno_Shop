using MediatR;
using UserManagement.Application.Common.CustomExceptions;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Features.AuthenticateFeatures.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, Unit>
    {
        private readonly IUserRepository _userRepository;

        public ResetPasswordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.email);
            if(user is null) 
            {
                throw new NotFoundException($"User with email {request.email} not found");
            }
            
            await _userRepository.ResetPasswordAsync(user,request.token,request.password);

            return Unit.Value;
        }
    }


}
