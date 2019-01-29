using Microsoft.Extensions.Options;
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

        //private static  IOptions<AppSettings> _settings;

        //public DataBaseConfig(IOptions<AppSettings> settings)
        //{
        //    _settings = settings;
        //}

        //private static DatabaseType GetDataBaseType(string databaseType)
        //{
        //    DatabaseType returnValue = DatabaseType.SqlServer;
        //    foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
        //    {
        //        if (dbType.ToString().Equals(databaseType, StringComparison.OrdinalIgnoreCase))
        //        {
        //            returnValue = dbType;
        //            break;
        //        }
        //    }
        //    return returnValue;
        //}

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        //    public static IDbConnection GetSqlConnection()
        //    {
        //        IDbConnection connection = null;
        //        //获取配置进行转换
        //        var type = _settings.Value.ComponentDbType;
        //        var dbType = GetDataBaseType(type);
        //        //DefaultDatabase 根据这个配置项获取对应连接字符串
        //        var database = _settings.Value.DefaultDatabase;
        //        if (string.IsNullOrEmpty(database))
        //        {
        //            database = "SqlServer";//默认配置
        //        }
        //        var strConn = _settings.Value.ConnectionStrings;
        //        switch (dbType)
        //        {
        //            case DatabaseType.SqlServer:
        //                connection = new System.Data.SqlClient.SqlConnection(strConn.ToString());
        //                break;
        //            case DatabaseType.MySql:
        //                connection = new MySql.Data.MySqlClient.MySqlConnection(strConn.ToString());
        //                break;
        //            case DatabaseType.Oracle:
        //                connection = new Oracle.ManagedDataAccess.Client.OracleConnection(strConn.ToString());
        //                break;
        //        }
        //        return connection;
        //    }
        //}


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
