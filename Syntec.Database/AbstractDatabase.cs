using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.IO;

namespace Syntec.Database
{
    public abstract class AbstractDatabase : IDatabase
    {
        private string _strUsername;
        private string _strPassword;
        private string _strDatabase;
        private string _strHost;
        private int _iPort;

        private bool _isAutoCommit;
        private string _strErrorMessage;
        protected DbConnection _conn;
        protected DbCommand _stmt;
        protected DbTransaction _transaction;
        private string _strParamPrefix = "";

        public string ParamPrefix
        {
            get { return _strParamPrefix; }
            set { _strParamPrefix = value; }
        }

        public int Port
        {
            get { return _iPort; }
            set { _iPort = value; }
        }

        public string Username
        {
            get { return _strUsername; }
            set { _strUsername = value; }
        }

        public string Password
        {
            get { return _strPassword; }
            set { _strPassword = value; }
        }

        public string Database
        {
            get { return _strDatabase; }
            set { _strDatabase = value; }
        }

        public string Host
        {
            get { return _strHost; }
            set { _strHost = value; }
        }

        public bool AutoCommit
        {
            get { return _isAutoCommit; }
            set { _isAutoCommit = value; }
        }

        public string ErrorMessage
        {
            get { return _strErrorMessage; }
            set { _strErrorMessage = value; }
        }        

        protected abstract DbConnection CreateConnection();
        protected abstract DbCommand CreateStatement();
        protected abstract string ConnectionString();

        public int ExecuteScriptFile(string strFilename)
        {
            string strScript = "";
            StreamReader sr = null;

            string strLine;

            try
            {
                //FileInfo file = new FileInfo(strFilename);

                //string script = file.OpenText().ReadToEnd();

                sr = new StreamReader(strFilename, Encoding.Default);

                while ((strLine = sr.ReadLine()) != null)
                {
                    if (strLine.Trim().StartsWith("--"))
                        continue;

                    if (String.IsNullOrEmpty(strLine))
                        continue;

                    strScript += strLine + Environment.NewLine;
                }

                return ExecuteScript(strScript);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace + "\n" + strFilename);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace + "\n" + strFilename;
            }
            finally
            {
                if(sr != null)
                    sr.Close();
            }
            return -1;

        }

        public abstract int ExecuteScript(string strSQLStatement);


        #region IDatabase Members
        public bool Open(string strConnection)
        {
            try
            {
                _conn = CreateConnection();
                _conn.Open();
                _stmt = CreateStatement();
                _stmt.Connection = _conn;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace;
            }
            return false;
        }

        public bool Open(string strDatabase, string strUsername, string strPassword)
        {
            return Open("local", strDatabase, strUsername, strPassword);
        }

        public bool Open(string strHost, string strDatabase, string strUsername, string strPassword)
        {
            try
            {
                _strHost = strHost;
                _strDatabase = strDatabase;
                _strUsername = strUsername;
                _strPassword = strPassword;

                return Open(ConnectionString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace;
            }
            return false;
        }

        public  void Close()
        {
            try
            {
                if (_conn != null)
                    _conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace;
            }

        }

        public  void BeginTransaction()
        {
            _transaction = _conn.BeginTransaction();
            _stmt.Transaction = _transaction;
        }

        public  void EndTransaction()
        {
            if (IsInTransaction())
                _transaction = null;
        }

        public  bool IsInTransaction()
        {
            return _transaction != null;
        }

        public  bool Commit()
        {
            try
            {
                if (IsInTransaction())
                    _transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message + "\n" + ex.StackTrace;
            }
            return false;
        }

        public  bool Rollback()
        {
            try
            {
                if (IsInTransaction())
                    _transaction.Rollback();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message + "\n" + ex.StackTrace;
            }
            return false;
        }



        public  DbDataReader ExecuteQuery(string strStmt, object[] oArgs)
        {
            return ExecuteQuery(String.Format(strStmt, oArgs));
        }

        public  DbDataReader ExecuteQuery(string strStmt)
        {
            
            if (_conn.State != ConnectionState.Open)
                _conn.Open();

            if (_stmt == null)
                return null;

            try
            {
                _stmt.CommandText = strStmt;
                return _stmt.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace + "\n" + strStmt);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace + "\n" + strStmt;
            }
            
            return null;
        }

        public DbDataReader ExecuteQuery(string strStmt, string[] strFields, object[] oArgs)
        {

            if (_conn.State != ConnectionState.Open)
                _conn.Open();
            
            if(_stmt == null)
                return null;

            try
            {
                DbParameter parameter;
                _stmt.CommandText = strStmt;
                _stmt.Parameters.Clear();
                if (strFields != null)
                {
                    for (int i = 0; i < strFields.Length; i++)
                    {
                        parameter = _stmt.CreateParameter();
                        parameter.ParameterName = ParamPrefix + strFields[i];

                        if (oArgs[i] == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                        else
                        {
                            parameter.Value = oArgs[i];
                        }

                        _stmt.Parameters.Add(parameter);
                    }
                }
                return _stmt.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace + "\n" + strStmt);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace + "\n" + strStmt;
            }

            return null;
        }


        public int ExecuteUpdate(string strStmt, string[] strFields, object[] oArgs)
        {
            if (_stmt == null)
                return -1;

            try
            {
                DbParameter parameter;
                _stmt.CommandText = strStmt;
                _stmt.Parameters.Clear();

                if (strFields != null)
                {
                    for (int i = 0; i < strFields.Length; i++)
                    {
                        parameter = _stmt.CreateParameter();
                        parameter.ParameterName = ParamPrefix + strFields[i];

                        if (oArgs[i] == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                        else
                        {
                            parameter.Value = oArgs[i];
                        }

                        _stmt.Parameters.Add(parameter);
                    }
                }
                return _stmt.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                 Console.WriteLine(ex.Message + "\n" + ex.StackTrace + "\n" + strStmt);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace + "\n" + strStmt;
            }
            return -1;
        }


        public  int ExecuteUpdate(string strStmt, object[] oArgs)
        {
            return ExecuteUpdate(String.Format(strStmt, oArgs));
        }

        public  int ExecuteUpdate(string strStmt)
        {
            if (_stmt == null)
                return -1;

            try
            {
                _stmt.CommandText = strStmt;
                return _stmt.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace + "\n" + strStmt);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace + "\n" + strStmt;
            }
            return -1;
        }

        public int ExecuteScalar(string strStmt, string[] strFields, object[] oArgs)
        {
            if (_stmt == null)
                return Int16.MaxValue;
            try
            {
                DbParameter parameter;
                _stmt.CommandText = strStmt;
                _stmt.Parameters.Clear();
                
                if (strFields != null)
                {
                    for (int i = 0; i < strFields.Length; i++)
                    {
                        parameter = _stmt.CreateParameter();
                        parameter.ParameterName = ParamPrefix + strFields[i];

                        if (oArgs[i] == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                        else
                        {
                            parameter.Value = oArgs[i];
                        }

                        _stmt.Parameters.Add(parameter);
                    }
                }
                return (int)_stmt.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace + "\n" + strStmt);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace + "\n" + strStmt;
            }
            return Int16.MaxValue;
        }


        public  int ExecuteScalar(string strStmt, object[] oArgs)
        {
            return ExecuteScalar(String.Format(strStmt, oArgs));
        }

        public  int ExecuteScalar(string strStmt)
        {
            if (_stmt == null)
                return Int16.MaxValue;
            try
            {
                _stmt.CommandText = strStmt;
                return (int)_stmt.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace + "\n" + strStmt);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace + "\n" + strStmt;
            }
            return Int16.MaxValue;
        }

        #endregion
        private DataTable GetSchema(string strType, string[] strCriteria)
        {
            DataTable dtReturn = null;
            try
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();

                if (strCriteria == null)
                    return _conn.GetSchema(strType);
                else
                    return _conn.GetSchema(strType, strCriteria);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Close();
            }

            return dtReturn;
        }

        public DataTable GetDatabaseList()
        {
            return GetSchema("DataBases", null);
        }

        public DataTable GetTableList()
        {
            return GetSchema("Tables", new string[] { null, null, null, "BASE TABLE" });
        }

        public DataTable GetViewList()
        {
            return GetSchema("Tables", new string[] { null, null, null, "VIEW" });
        }

        public DataTable GetIndexList()
        {
            return GetSchema("Indexes", null);
        }

        public DataTable GetTableColumnList(string strTableName)
        {
            return GetTableColumnList(null, null, strTableName);
        }

        public DataTable GetTableColumnList(string strSchema, string strTableName)
        {
            return GetTableColumnList(null, strSchema, strTableName);
        }

        public DataTable GetTableColumnList(string strCatalog, string strSchema, string strTableName)
        {
            return GetSchema("Columns", new string[] { strCatalog, strSchema, strTableName, null });
        }

        public DataTable GetIndexColumnList(string strTableName)
        {
            return GetIndexColumnList(null, null, strTableName);
        }

        public DataTable GetIndexColumnList(string strSchema, string strTableName)
        {
            return GetIndexColumnList(null, strSchema, strTableName);
        }

        public DataTable GetIndexColumnList(string strCatalog, string strSchema, string strTableName)
        {
            return GetSchema("IndexColumns", new string[] { strCatalog, strSchema, strTableName, null });
        }


        public abstract string ToDate(string strDate);
    }
}
