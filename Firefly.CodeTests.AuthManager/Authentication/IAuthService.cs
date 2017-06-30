namespace Firefly.CodeTests.AuthManager
{
    public interface IAuthService<out TAuthResult>
    {
        TAuthResult AuthenticateUser(string username, string password);
    }
}