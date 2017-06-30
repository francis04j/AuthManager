using Firefly.CodeTests.AuthManager.Database;
using Firefly.CodeTests.AuthManager.Hashing;

namespace Firefly.CodeTests.AuthManager.Authentication
{
    public class AuthService : IAuthService<bool>
    {
        private readonly IPasswordHashService _passwordHashService;
        private readonly IUserCredentialRepository _credentialRepository;

        public AuthService(IUserCredentialRepository credentialRepository, IPasswordHashService passwordHashService)
        {
            _credentialRepository = credentialRepository;
            _passwordHashService = passwordHashService;
        }

        public bool AuthenticateUser(string username, string password)
        {
            var credential = _credentialRepository.FindByUserName(username);
            if (credential != null)
            {
                HashResult hashResult = _passwordHashService.Hash(password);
                if (credential.PasswordHash == hashResult.PasswordHash)
                {
                    return true;
                }
            }
            return false;
        }
    }
}