namespace Firefly.CodeTests.AuthManager.Hashing
{
    public class FakePasswordHashService : IPasswordHashService
    {
        public HashResult Hash(string password)
        {
            return new HashResult {PasswordSalt = "Salt", PasswordHash = password};
        }
    }
}