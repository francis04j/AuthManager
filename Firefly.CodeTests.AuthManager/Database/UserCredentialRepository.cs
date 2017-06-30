using System;
using System.Data;
using System.Data.SqlClient;
using Firefly.CodeTests.AuthManager.Hashing;
using Firefly.CodeTests.AuthManager.Model;

namespace Firefly.CodeTests.AuthManager.Database
{
    public class UserCredentialRepository : IUserCredentialRepository
    {
        private readonly SqlConnectionWrapper _sqlConnectionWrapper;
        private readonly IPasswordHashService _passwordHashService;

        public UserCredentialRepository(SqlConnectionWrapper sqlConnectionWrapper, IPasswordHashService passwordHashService)
        {
            _sqlConnectionWrapper = sqlConnectionWrapper;
            _passwordHashService = passwordHashService;
        }

       public void AddUserCredential(string username, string password)
        {
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "CreateUserCredential"
            };
            var hashResult = _passwordHashService.Hash(password);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar).Value = hashResult.PasswordHash;
            cmd.Parameters.Add("@PasswordSalt", SqlDbType.NVarChar).Value = hashResult.PasswordSalt;

            _sqlConnectionWrapper.ExecuteNonQueryCommand(cmd);
        }

        public UserCredential FindByUserName(string username)
        {
            var cmd = new SqlCommand("GetUserCredentialByUsername") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
            var result = _sqlConnectionWrapper.ExecuteQueryCommand(cmd, BuildFromDataReader);
            return result!=null? new UserCredential(username, result.hash, result.salt) : null;
        }

        private dynamic BuildFromDataReader(SqlDataReader reader)
        {
            return new {hash = reader.GetString(0), salt= reader.GetString(1)};
        }
    }
}