using Firefly.CodeTests.AuthManager.Database;
using Firefly.CodeTests.AuthManager.Hashing;
using Firefly.CodeTests.AuthManager.Model;
using Firefly.CodeTests.AuthManager.Validators;

namespace Firefly.CodeTests.AuthManager.Factory
{
    public class UserFactory : IUserFactory
    {
        private readonly IPasswordValidator _passwordValidator;
        private readonly IUsernameValidator _usernameValidator;
        private readonly IUserRepository _userRepository;
        private readonly IUserCredentialRepository _credentialRepository;

        public UserFactory(IPasswordValidator passwordValidator, IUsernameValidator usernameValidator, IUserRepository userRepository, IUserCredentialRepository credentialRepository)
        {
            _passwordValidator = passwordValidator;
            _usernameValidator = usernameValidator;
            _userRepository = userRepository;
            _credentialRepository = credentialRepository;
        }

        public User CreateAuthenticatedUser(string username, string password)
        {
            if (_usernameValidator.ValidateUserName(username) && _passwordValidator.ValidatePassword(password))
            {
                _userRepository.AddUser(new FireFlyUser(username, password));
                _credentialRepository.AddUserCredential(username, password);
                return new FireFlyUser(username, password);
            }
            return null;
        }
    }
}