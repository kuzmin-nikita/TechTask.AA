using FluentValidation;
using MediatR;
using TechTask.AA.Core.Exceptions;
using TechTask.AA.Core.Helpers;
using TechTask.AA.Core.Ports.Repositories;

namespace TechTask.AA.Application.Commands
{
    public record AuthorizeCommand(
        string Username,
        string Password) : IRequest<AuthorizeCommand.Result>
    {
        public record Result(string Token);

        public class Validator : AbstractValidator<AuthorizeCommand>
        {
            public Validator()
            {
                RuleFor(d => d.Username).NotNull().Length(1, 256);
                RuleFor(d => d.Password).NotNull().Length(1, 256);
            }
        }

        public class Handler : IRequestHandler<AuthorizeCommand, Result>
        {
            private readonly IUserRepository _userRepository;
            private readonly IRoleRepository _roleRepository;

            public Handler(IUserRepository userRepository, IRoleRepository roleRepository)
            {
                _userRepository = userRepository;
                _roleRepository = roleRepository;
            }

            public async Task<Result> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserByUsernameAsync(request.Username, cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException($"User with Username: '{request.Username}' was not found");
                }

                var requestPaswordHash = HashHelper.ComputeHash(request.Password);

                if (user.Password != requestPaswordHash)
                {
                    throw new BadRequestException($"Incorrect password for user with Username: '{request.Username}'");
                }

                var role = await _roleRepository.GetRoleByIdAsync(user.RoleId, cancellationToken);

                if (role == null)
                {
                    throw new NotFoundException($"Role with Id: '{user.RoleId}' was not found");
                }

                var jwt = JWTHelper.CreateToken(user.Username, role.Code);

                var result = new Result(Token: jwt);

                return result;
            }
        }
    }
}
