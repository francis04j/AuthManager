namespace Firefly.CodeTests.AuthManager.Model
{
    public class UserCredential
    {
        public UserCredential(string username, string passwordHash, string passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Username = username;
        }

        public string Username { get; }
        public string PasswordHash { get; }
        public string PasswordSalt { get; }
    }
}