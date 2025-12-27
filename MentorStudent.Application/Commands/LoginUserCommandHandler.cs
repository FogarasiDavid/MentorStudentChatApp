using MediatR;
using MentorStudent.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MentorStudent.Application.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHash _passwordHash;

        public LoginUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordHash passwordHash)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _passwordHash = passwordHash;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Hibás email vagy jelszó.");
            }

            bool isValid = _passwordHash.Verify(request.Password, user.PasswordHash);
            if (!isValid)
            {
                throw new UnauthorizedAccessException("Hibás email vagy jelszó.");
            }

            return _jwtProvider.Generate(user);
        }
    }
}