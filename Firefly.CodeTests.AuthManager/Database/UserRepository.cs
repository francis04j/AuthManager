using System.Data;
using System.Data.SqlClient;
using Firefly.CodeTests.AuthManager.Model;

namespace Firefly.CodeTests.AuthManager.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnectionWrapper _sqlConnectionWrapper;

        public UserRepository(SqlConnectionWrapper sqlConnectionWrapper)
        {
            _sqlConnectionWrapper = sqlConnectionWrapper;
        }

        public void AddUser(User user)
        {
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "CreateUser"
            };
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
            _sqlConnectionWrapper.ExecuteNonQueryCommand(cmd);
        }

        public User FindByUserName(string username)
        {
           var cmd = new SqlCommand("GetUserByUsername") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;

            var result = _sqlConnectionWrapper.ExecuteQueryCommand(cmd, Func);
            return result;
        }

        private User Func(SqlDataReader sqlDataReader)
        {
            return new FireFlyUser(sqlDataReader.GetString(0), string.Empty);
        }
    }
}