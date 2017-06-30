using System;
using System.ComponentModel.DataAnnotations;
using Firefly.CodeTests.AuthManager.Database;

namespace Firefly.CodeTests.AuthManager.Validators
{
    public class UsernameValidator : IUsernameValidator
    {
        private readonly IActiveDirectoryAccountValidator _activeDirectoryAccountValidator;
        private readonly IUserRepository _userRepository;

        public UsernameValidator(IActiveDirectoryAccountValidator activeDirectoryAccountValidator, IUserRepository userRepository)
        {
            _activeDirectoryAccountValidator = activeDirectoryAccountValidator;
            _userRepository = userRepository;
        }

        public bool ValidateUserName(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            CheckIfUsernameIsValidEmail(username);

            CheckIfUsernameIsAdCompliant(username);

            CheckIfUserExistInDb(username);

            return true;
        }

        private static void CheckIfUsernameIsValidEmail(string username)
        {
            if (!new EmailAddressAttribute().IsValid(username))
            {
                throw new ArgumentException("username is not a valid email address");
            }
        }

        private void CheckIfUsernameIsAdCompliant(string username)
        {
            if (!_activeDirectoryAccountValidator.ValidateUser(username))
            {
                throw new ArgumentException("username is not a valid active directory account");
            }
        }

        private void CheckIfUserExistInDb(string username)
        {
            if (_userRepository.FindByUserName(username) != null)
            {
                throw new ArgumentException($"user with username {username} already exists");
            }
        }
    }
}