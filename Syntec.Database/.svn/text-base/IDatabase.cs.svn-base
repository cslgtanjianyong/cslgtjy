using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace Syntec.Database
{
    public interface IDatabase
    {
        string ToDate(string strDate);
        

        bool Open(string strConnection);
        bool Open(string strDatabase, string strUsername, string strPassword);
        bool Open(string strHost, string strDatabase, string strUsername, string strPassword);

        void Close();
        string ParamPrefix { get; set; }
        string ErrorMessage { get; set; }
        bool AutoCommit { get; set; }

        void BeginTransaction();
        void EndTransaction();
        bool IsInTransaction();
        bool Commit();
        bool Rollback();
        
        DbDataReader ExecuteQuery(string strStmt, string[] strFields, object[] oArgs);
        DbDataReader ExecuteQuery(string strStmt, object[] oArgs);
        DbDataReader ExecuteQuery(string strStmt);
        int ExecuteUpdate(string strStmt, string[] strFields, object[] oArgs);
        int ExecuteUpdate(string strStmt, object[] oArgs);
        int ExecuteUpdate(string strStmt);
        int ExecuteScalar(string strStmt, string[] strFields, object[] oArgs);
        int ExecuteScalar(string strStmt, object[] oArgs);
        int ExecuteScalar(string strStmt);

        int ExecuteScriptFile(string strFilename);
        int ExecuteScript(string strSQLStatement);

        DataTable GetDatabaseList();
        DataTable GetTableList();
        DataTable GetViewList();
        DataTable GetIndexList();
        DataTable GetTableColumnList(string strTableName);
        DataTable GetTableColumnList(string strSchema, string strTableName);
        DataTable GetTableColumnList(string strCatalog, string strSchema, string strTableName);

        DataTable GetIndexColumnList(string strTableName);
        DataTable GetIndexColumnList(string strSchema, string strTableName);
        DataTable GetIndexColumnList(string strCatalog, string strSchema, string strTableName);

    }
}
