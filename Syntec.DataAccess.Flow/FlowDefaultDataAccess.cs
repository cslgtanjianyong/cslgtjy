using System;
using System.Collections.Generic;
using System.Text;
using Syntec.Database;
using Syntec.DataAccess.Meta;

namespace Syntec.DataAccess.Flow
{
    public class FlowDefaultDataAccess : AbstractDatabaseDataAccess
    {
        public FlowDefaultDataAccess()
        {
            DatabaseInfo databaseInfo = new DatabaseInfo();
            databaseInfo.Host = "18.18.1.13";
            databaseInfo.Database = "EFGP";
            databaseInfo.Username = "dscsa";
            databaseInfo.Password = "dsc";
            SetConfig(databaseInfo);

        }
        protected override Syntec.Database.IDatabase CreateDatabase()
        {
            return new MSSqlDatabase();
        }
    }
}
