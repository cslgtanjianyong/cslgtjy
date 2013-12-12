using System;
using System.Collections.Generic;
using System.Text;
using Syntec.DataAccess.Meta;
using Syntec.Database;

namespace Syntec.DataAccess.Watchman
{
    public class WatchmanDefaultDataAccess : AbstractDatabaseDataAccess
    {
        public WatchmanDefaultDataAccess()
        {
            DatabaseInfo databaseInfo = new DatabaseInfo();
            databaseInfo.Host = "www.syntecclub.com.tw,29";
            databaseInfo.Database = "Watchman";
            databaseInfo.Username = "sunsa";
            databaseInfo.Password = "syntec0318";
            SetConfig(databaseInfo);

        }
        protected override Syntec.Database.IDatabase CreateDatabase()
        {
            return new MSSqlDatabase();
        }
    }
}
