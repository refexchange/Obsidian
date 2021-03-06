using Obsidian.Domain;
using Obsidian.Domain.Repositories;
using Obsidian.Foundation.ProcessManagement;
using System.Threading.Tasks;

namespace Obsidian.Application.Authentication
{
    public class PasswordAuthenticateSaga : Saga,
                                      IStartsWith<PasswordAuthenticateCommand, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;

        public PasswordAuthenticateSaga(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        public Task<AuthenticationResult> StartAsync(PasswordAuthenticateCommand command)
        {
            User user;
            return Task.FromResult(new AuthenticationResult
            {
                IsCredentialValid = TryLoadUser(command.UserName, out user)
                && user.VaildatePassword(command.Password),
                User = user
            });
        }

        protected override bool IsProcessCompleted() => true;

        private bool TryLoadUser(string userName, out User user)
        {
            user = _userRepository.FindByUserNameAsync(userName).Result;
            return user != null;
        }
    }
}