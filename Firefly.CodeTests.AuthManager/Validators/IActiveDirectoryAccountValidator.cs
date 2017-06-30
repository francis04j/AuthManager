namespace Firefly.CodeTests.AuthManager.Validators
{
    public  interface IActiveDirectoryAccountValidator
    {
        bool ValidateUser(string username);
    }
}