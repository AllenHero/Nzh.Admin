using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Nzh.Admin.Common.Base
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DataBaseConfig
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string DefaultSqlConnectionString = @"Data Source=localhost;Initial Catalog=STD_DB;User ID=sa;Password=123;";

        /// <summary>
        /// 获取数据库连接信息
        /// </summary>
        /// <param name="sqlConnectionString"></param>
        /// <returns></returns>
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
