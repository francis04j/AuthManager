namespace Firefly.CodeTests.AuthManager.Hashing
{
    public interface IPasswordHashService
    {
        HashResult Hash(string password);
    }
}