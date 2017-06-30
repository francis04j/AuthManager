namespace Firefly.CodeTests.AuthManager.Model
{
    public class FireFlyUser : User
    {
        public FireFlyUser(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public override string Username { get; }
        public override string Password { get; }
    }
}