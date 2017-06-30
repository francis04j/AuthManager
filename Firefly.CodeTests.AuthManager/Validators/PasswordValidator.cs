namespace Firefly.CodeTests.AuthManager.Validators
{
    public class PasswordValidator : IPasswordValidator
    {
        public bool ValidatePassword(string password)
        {
            return !string.IsNullOrEmpty(password);
        }
    }
}