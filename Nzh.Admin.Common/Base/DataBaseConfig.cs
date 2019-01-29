using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Nzh.Admin.Common.Base
{
    public class DataBaseConfig
    {
        private static string DefaultSqlConnectionString = @"Data Source=localhost;Initial Catalog=STD_DB;User ID=sa;Password=123;";
        private static string DefaultRedisString = "localhost, abortConnect=false";
        private static ConnectionMultiplexer redis;

        public static IDbConnection GetSqlConnection(string sqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                sqlConnectionString = DefaultSqlConnectionString;
            }
            IDbConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
    }
}
