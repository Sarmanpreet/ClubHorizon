
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

namespace GolfHorizon.Data.Dapper
{
    public class ConnectionStringManager
    {


        public static string ConnectionString;

        public static string GetAdminConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AdminConnection"].ConnectionString;
        }
        public string GetAdminConnection()
        {
            return ConfigurationManager.ConnectionStrings["AdminConnection"].ConnectionString;
        }
    }
    public static class DataLayer
    {
        public static SqlConnection sqlCreateConnection()
        {
            ConnectionStringManager conn = new ConnectionStringManager();
            //IDbConnection _db = conn.GetConnectionString();
            SqlConnection _db = new SqlConnection(conn.GetAdminConnection());
            return _db;
        }

        public static IDbConnection CreateDynamicDataBaseConnection(string connectionString)
        {
            IDbConnection _db = new SqlConnection(connectionString);
            return _db;
        }

        //public static bool IsValidDBConnection(string connectionString)
        //{
        //    try
        //    {
        //        int ReturnData = -1;
        //        DapperExecuteServiceFromAnyDB<int> svr = new DapperExecuteServiceFromAnyDB<int>(new ConnectionFromStringModel(connectionString));
        //        string sqlQuery = @"SELECT CAST(1 AS INT) ReturnData";
        //        ReturnData = svr.ExecuteSingleObjectSqlQuery(sqlQuery);
        //        if (ReturnData <= 0)
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
