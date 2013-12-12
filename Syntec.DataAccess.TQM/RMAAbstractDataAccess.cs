using System;
using System.Collections.Generic;
using System.Text;
using Syntec.Database;
using Syntec.DataAccess.Meta;

namespace Syntec.DataAccess.TQM
{
    public abstract class RMAAbstractDataAcces : AbstractDatabaseDataAccess
    {
        public RMAAbstractDataAcces()
        {
            //ConfigFile = "Syntec.DataAccess.TQM.dll;Syntec.DataAccess.TQM.system.ini";
            DatabaseInfo databaseInfo = new DatabaseInfo();
            databaseInfo.Host = "crm.syntecclub.com.tw,29";
            databaseInfo.Database = "SyntecBarCode";
            databaseInfo.Username = "sunsa";
            databaseInfo.Password = "syntec0318";
            SetConfig(databaseInfo);
        }

        protected override IDatabase CreateDatabase()
        {                        
            return new MSSqlDatabase();
        }        
    }
}
