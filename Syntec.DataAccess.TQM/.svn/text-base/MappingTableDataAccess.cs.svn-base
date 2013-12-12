using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class MappingTableDataAccess : RMAAbstractDataAcces
    {
        public const int DETAIL = 1;
        public const int NAME_FILTER = 2;

        public MappingTableDataAccess(string strFieldName, string strTableName)
        {
            string strSQL;

            strSQL = "SELECT " + strFieldName
                   + "  FROM " + strTableName
                   + " ORDER BY " + strFieldName;
            AddSQLStatement(DETAIL, strSQL);
        }
    }
}
