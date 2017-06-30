using Firefly.CodeTests.AuthManager.Model;

namespace Firefly.CodeTests.AuthManager.Database
{
    public interface IUserCredentialRepository
    {
        void AddUserCredential(string username, string password);
        UserCredential FindByUserName(string username);
    }
}