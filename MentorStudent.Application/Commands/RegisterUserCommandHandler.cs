using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MentorStudent.Application.Interfaces;
using MentorStudent.Domain.Entities;
using MentorStudent.Domain.ValueObjects;
using MediatR;

namespace MentorStudent.Application.Commands
{
    public class RegisterUserCommandHandler
     : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHash _passwordHasher;
        private readonly IJwtProvider _jwtProviderer;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHash passwordHasher, IJwtProvider jwtProviderer)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProviderer = jwtProviderer;
        }

        public async Task<string> Handle(RegisterUserCommand command, CancellationToken ct)
        {
            var existing = await _userRepository.GetByEmailAsync(command.Email);
            if (existing != null)
            {
                throw new InvalidOperationException("Az email már létezik");
            }
            var email = Email.Create(command.Email);
            var hash = _passwordHasher.Hash(command.Password);

            var user = new User
                (
                 Guid.NewGuid(),
                 email,
                 hash,
                 command.Role
                );
            await _userRepository.AddAsync(user);
            return _jwtProviderer.Generate(user);

        }

    }
}
