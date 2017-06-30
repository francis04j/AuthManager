using System.DirectoryServices.AccountManagement;

namespace Firefly.CodeTests.AuthManager.Validators
{
    public class ActiveDirectoryAccountValidator : IActiveDirectoryAccountValidator
    {
        public bool ValidateUser(string username)
        {
            bool validUser = false;
            using (var ctx = new PrincipalContext(ContextType.Domain))
            {
                var user = UserPrincipal.FindByIdentity(ctx, username);

                if (user != null)
                {
                    validUser = true;
                    user.Dispose();
                }
            }
            return validUser;
        }
    }
}