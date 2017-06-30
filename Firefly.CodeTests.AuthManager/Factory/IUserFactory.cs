using Firefly.CodeTests.AuthManager.Model;

namespace Firefly.CodeTests.AuthManager.Factory
{
    public interface IUserFactory
    {
        User CreateAuthenticatedUser(string username, string password);
    }
}