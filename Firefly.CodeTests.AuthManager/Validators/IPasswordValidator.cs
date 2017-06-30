namespace Firefly.CodeTests.AuthManager.Validators
{
    public interface IPasswordValidator
    {
        bool ValidatePassword(string password);
    }
}