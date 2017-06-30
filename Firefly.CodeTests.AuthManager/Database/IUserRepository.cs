using Firefly.CodeTests.AuthManager.Model;

namespace Firefly.CodeTests.AuthManager.Database
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User FindByUserName(string username);
    }
}