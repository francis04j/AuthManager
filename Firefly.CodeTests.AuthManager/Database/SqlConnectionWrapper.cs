using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Firefly.CodeTests.AuthManager.Database
{
    public class SqlConnectionWrapper
    {
        private readonly string _connectionString;
        private SqlConnection _sqlConnection;
        public SqlConnectionWrapper(string connectionString)
        {
            _connectionString = connectionString;
            _sqlConnection = new SqlConnection(_connectionString);
        }

        public void ExecuteNonQueryCommand(SqlCommand command)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                command.Connection = _sqlConnection;
                using (command)
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public TResult ExecuteQueryCommand<TResult>(SqlCommand command, Func<SqlDataReader, TResult> resultBuilder)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                command.Connection = _sqlConnection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return resultBuilder(reader);
                }
                return default(TResult);
            }
        }

    }
}