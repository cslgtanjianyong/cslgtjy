using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace Syntec.Database
{
    public class MSSqlDatabase : AbstractDatabase
    {
        #region Protected function
        public override int ExecuteScript(string strSQLStatement)
        {
            try
            {
                if (_conn == null)
                {
                    ErrorMessage = "The connection object is null!!";
                    return -1;
                }

                if (_conn.State != ConnectionState.Open)
                {
                    ErrorMessage = "The connection is closed!!";
                    return -1;
                }

                Server server = new Server(new ServerConnection((SqlConnection)_conn));
                

                string[] singleCommand = Regex.Split(strSQLStatement, "^GO", RegexOptions.Multiline);
                StringCollection scl = new StringCollection();
                int[] iResults = null;
                foreach (string t in singleCommand)
                {
                    if (t.Trim().Length > 0)
                        scl.Add(t.Trim().Replace(Environment.NewLine, " "));
                }

                try
                {
                    server.ConnectionContext.InfoMessage += new SqlInfoMessageEventHandler(ConnectionContext_InfoMessage);
                    server.ConnectionContext.StatementExecuted += new StatementEventHandler(ConnectionContext_StatementExecuted);
                    server.ConnectionContext.ServerMessage += new ServerMessageEventHandler(ConnectionContext_ServerMessage);
                    
                    Console.WriteLine("BatchSeparator : " + server.ConnectionContext.BatchSeparator);
                    Console.WriteLine("Captured Sql : " + server.ConnectionContext.CapturedSql);
                    iResults = server.ConnectionContext.ExecuteNonQuery(scl, ExecutionTypes.Default);
                    
                    // Now check the result array to find any possible errors??
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message + "\n" + ex.StackTrace;
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    //handling and logging for the errors are done here
                }

                if (iResults != null)
                {
                    foreach (int iResult in iResults)
                    {
                        if (iResult >= 0)
                        {
                            //Rollback();
                            return iResult;
                        }
                    }
                }
                //Commit();
                //return server.ConnectionContext.ExecuteNonQuery(strSQLStatement);
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace + "\n" + strSQLStatement);
                ErrorMessage = ex.Message + "\n" + ex.StackTrace + "\n" + strSQLStatement;

            }
            return -1;
        }

        void ConnectionContext_ServerMessage(object sender, ServerMessageEventArgs e)
        {
            Console.WriteLine("ServerMessage : " +  e.Error);
        }

        void ConnectionContext_StatementExecuted(object sender, StatementEventArgs e)
        {
            Console.WriteLine("Statement Executed : " + e.SqlStatement);
        }

        void ConnectionContext_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            Console.WriteLine("InfoMessage : " + e.Source + "\n" + e.Errors + "\n" + e.Message);
            ErrorMessage = e.Message;
        }

        protected override DbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString());
        }
        protected override DbCommand CreateStatement()
        {
            return new SqlCommand();
        }

        protected override string ConnectionString()
        {
            return String.Format("Data Source = {0}; Initial catalog = {1}; User id = {2}; Password = {3}",
                                 Host, Database, Username, Password);
        }
        #endregion

        public override string ToDate(string strDate)
        {
            return "'" + strDate + "'";
        }
    }

}
