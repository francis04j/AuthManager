using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Firefly.CodeTests.AuthManager.Model;

namespace Firefly.CodeTests.AuthManager.Database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;
        
        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }
        

        public void ClearTestData()
        {
            string procName = "ClearTestData";
            ExecuteStoreProcedure(procName);
        }
        private void ExecuteStoreProcedure(string procName)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand(procName, _sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        
    }
}