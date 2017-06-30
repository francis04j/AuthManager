namespace Firefly.CodeTests.AuthManager.Hashing
{
    public struct HashResult
    {
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}